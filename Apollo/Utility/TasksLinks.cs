using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Microsoft.Net.Http.Headers;

namespace Apollo.Utility;

public class TasksLinks : ITasksLinks
{
    //TODO: Done
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<TasksDto> _dataShaper;
    
    public Dictionary<string, MediaTypeHeaderValue> AcceptHeader { get; set; } =
        new Dictionary<string, MediaTypeHeaderValue>();

    public TasksLinks(LinkGenerator linkGenerator, IDataShaper<TasksDto> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }
    
    public LinkResponse TryGenerateLinks(IEnumerable<TasksDto> tasksDto, 
        string fields, Guid employeeId, HttpContext httpContext)
    {
        var shapedTasks = ShapeData(tasksDto, fields);

        return ShouldGenerateLinks(httpContext)
            ? ReturnLinkedTasks(tasksDto, fields, employeeId, httpContext, shapedTasks)
            : ReturnShapedTasks(shapedTasks);
    }
    
    private List<Entity> ShapeData(IEnumerable<TasksDto> tasksDto, string fields) =>
        _dataShaper.ShapeData(tasksDto, fields)
            .Select(e => e.Entity)
            .ToList();

    private static bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

        return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    
    private static LinkResponse ReturnShapedTasks(List<Entity> shapedTasks) =>
        new() { ShapedEntities = shapedTasks };

    private LinkResponse ReturnLinkedTasks(IEnumerable<TasksDto> tasksDto,
        string fields, Guid employeeId, HttpContext httpContext, List<Entity> shapedTasks)
    {
        var taskDtoList = tasksDto.ToList();

        for (var index = 0; index < taskDtoList.Count; index++)
        {
            var taskLinks = CreateLinksForTask(httpContext, employeeId, taskDtoList[index].Id, fields);
            shapedTasks[index].Add("Links", taskLinks);
        }
        
        var taskCollection = new LinkCollectionWrapper<Entity>(shapedTasks);
        
        var linkedTasks = CreateLinksForTasks(httpContext, taskCollection);
        
        return new LinkResponse(){ HasLinks = true, LinkedEntities = linkedTasks };
    }
    
    private List<Link> CreateLinksForTask(HttpContext httpContext, Guid employeeId, Guid taskId, string fields = "")
    {
        var links = new List<Link>
        {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetTaskForEmployee",
                    values: new { employeeId, taskId, fields }),
                "self",
                "GET"),
            
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteTaskForEmployee", 
                    values: new { employeeId, taskId }),
                "delete_task",
                "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateTaskForEmployee",
                    values: new { employeeId, taskId }),
                "update_task",
                "PUT"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateTaskForEmployee", 
                    values: new { employeeId, taskId }),
                "partially_update_task",
                "PATCH")
        };

        return links;
    }
    
    private LinkCollectionWrapper<Entity> CreateLinksForTasks(HttpContext httpContext, 
        LinkCollectionWrapper<Entity> tasksWrapper)
    {
        tasksWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
                "GetTasksForEmployee", 
                values: new { }),
            "self",
            "GET"));
        
        return tasksWrapper;
    }
}