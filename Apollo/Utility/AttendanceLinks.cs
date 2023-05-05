using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace Apollo.Utility;

public class AttendanceLinks : IAttendanceLinks
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<AttendanceDto> _dataShaper;

    public AttendanceLinks(LinkGenerator linkGenerator, IDataShaper<AttendanceDto> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }

    public LinkResponse TryGenerateLinks(IEnumerable<AttendanceDto> attendancesDto, 
        string? fields, Guid? employeeId,
        Guid? companyId, HttpContext httpContext)
    {
        var attendanceDtos = attendancesDto.ToList();

        var shapedAttendances = ShapeData(attendanceDtos, fields!);
        return ShouldGenerateLinks(httpContext)
            ? ReturnLinkedAttendances(httpContext, shapedAttendances)
            : ReturnShapedAttendances(shapedAttendances);
    }

    private List<Entity> ShapeData(IEnumerable<AttendanceDto> attendancesDto, string fields) =>
        _dataShaper.ShapeData(attendancesDto, fields).Select(e => e.Entity).ToList();

    private static bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var items = httpContext.Items;
        if (!items.TryGetValue("AcceptHeaderMediaType", out var value)) return false;
        var mediaType = value as MediaTypeHeaderValue;
        return mediaType != null && mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }

    private static LinkResponse ReturnShapedAttendances(List<Entity> shapedAttendances) =>
        new() { ShapedEntities = shapedAttendances };

    private LinkResponse ReturnLinkedAttendances( HttpContext httpContext, List<Entity> shapedAttendances)
    {
        var attendanceCollection = new LinkCollectionWrapper<Entity>(shapedAttendances);
        var linkedAttendances = CreateLinksForAttendances(httpContext, attendanceCollection);

        return new LinkResponse { HasLinks = true, LinkedEntities = linkedAttendances };
    }
    
    private LinkCollectionWrapper<Entity> CreateLinksForAttendances(HttpContext httpContext,
        LinkCollectionWrapper<Entity> attendanceWrapper)
    {
        attendanceWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
                "GetEmployeesAttendanceByCompanyId",
                "Attendance", values: new { })!,
            "self",
            "GET"));
        
        attendanceWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
                "GetEmployeeAttendances",
                "Attendance", values: new { })!,
            "self",
            "GET"));

        return attendanceWrapper;
    }
}