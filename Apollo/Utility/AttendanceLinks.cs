using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Microsoft.Net.Http.Headers;

namespace Apollo.Utility;

public class AttendanceLinks : IAttendanceLinks
{
    //Todo : Add AttendanceLinks
    
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<AttendanceDto> _dataShaper;
    
    public Dictionary<string, MediaTypeHeaderValue> AcceptHeader { get; set; } =
        new Dictionary<string, MediaTypeHeaderValue>();

    public AttendanceLinks(LinkGenerator linkGenerator, IDataShaper<AttendanceDto> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }
    public LinkResponse TryGenerateLinks(IEnumerable<AttendanceDto> attendancesDto,
        string fields, Guid employeeId, HttpContext httpContext)
    {
        var attendanceDtos = attendancesDto as AttendanceDto[] ?? attendancesDto.ToArray();
        var shapedTasks = ShapeData(attendanceDtos, fields);
        
        return ShouldGenerateLinks(httpContext)
            ? ReturnLinkedTasks(attendanceDtos, fields, employeeId, httpContext, shapedTasks)
            : ReturnShapedTasks(shapedTasks);
    }
    
    private List<Entity> ShapeData(IEnumerable<AttendanceDto> attendancesDto, string fields) =>
        _dataShaper.ShapeData(attendancesDto, fields)
            .Select(e => e.Entity)
            .ToList();
    
    private static bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

        return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    
    private static LinkResponse ReturnShapedTasks(List<Entity> shapedTasks) =>
        new() { ShapedEntities = shapedTasks };

    private LinkResponse ReturnLinkedTasks(IEnumerable<AttendanceDto> attendancesDto,
        string fields, Guid employeeId, HttpContext httpContext, List<Entity> shapedTasks)
    {
        var attendanceDtoList = attendancesDto.ToList();
        
        for (var index = 0; index < attendanceDtoList.Count; index++)
        {
            var attendanceLinks = CreateLinksForAttendance(httpContext, employeeId, attendanceDtoList[index].Id, fields);
            shapedTasks[index].Add("Links", attendanceLinks);
        }
        
        var attendanceCollection = new LinkCollectionWrapper<Entity>(shapedTasks);
        
        var linkedAttendances = CreateLinksForAttendances(httpContext, attendanceCollection);
        
        return new LinkResponse { HasLinks = true, LinkedEntities = linkedAttendances };
    }

    private List<Link> CreateLinksForAttendance(HttpContext httpContext, Guid employeeId, Guid attendanceId,
        string fields = "")
    {
        var links = new List<Link>
        {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetAttendanceForEmployee",
                    values: new { employeeId, attendanceId, fields }),
                "self",
                "GET")
        };

        return links;
    }
    
    private LinkCollectionWrapper<Entity> CreateLinksForAttendances(HttpContext httpContext,
        LinkCollectionWrapper<Entity> attendanceWrapper)
    {
        attendanceWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
                "GetAttendancesForEmployee",
                values: new {  }),
            "self",
            "GET"));

        return attendanceWrapper;
    }
}