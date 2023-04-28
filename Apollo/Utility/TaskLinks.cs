using Microsoft.Net.Http.Headers;
using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Apollo.Utility;

public class TaskLinks : ITaskLinks
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<TasksDto> _dataShaper;

    public TaskLinks(LinkGenerator linkGenerator, IDataShaper<TasksDto> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }

    public LinkResponse TryGenerateLinks(IEnumerable<TasksDto> tasksDto, string fields, Guid employeeId, HttpContext httpContext)
    {
        var tasksDtos = tasksDto.ToList();

        var shapedTasks = ShapeData(tasksDtos, fields);
        return ShouldGenerateLinks(httpContext)
            ? ReturnLinkedTasks(tasksDtos, fields, employeeId, httpContext, shapedTasks)
            : ReturnShapedTasks(shapedTasks);
    }

    private List<Entity> ShapeData(IEnumerable<TasksDto> tasksDtos, string fields) =>
        _dataShaper.ShapeData(tasksDtos, fields).Select(e => e.Entity).ToList();

    private static bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"]!;

        return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    
    private static LinkResponse ReturnShapedTasks(List<Entity> shapedTasks) =>
        new LinkResponse { ShapedEntities = shapedTasks };

    private LinkResponse ReturnLinkedTasks(IEnumerable<TasksDto> tasksDtos, string fields, Guid employeeId,
        HttpContext httpContext, List<Entity> shapedTasks)
    {
        var taskDtoList = tasksDtos.ToList();
        
        for (var index = 0; index < taskDtoList.Count; index++)
        {
            var taskLinks = CreateLinksForTask(httpContext, employeeId, taskDtoList[index].Id, fields);
            shapedTasks[index].Add("links", taskLinks);
        }
        
        var taskCollection = new LinkCollectionWrapper<Entity>(shapedTasks);
        var linkedTasks = CreateLinksForTasks(httpContext, taskCollection);
        
        return new LinkResponse { HasLinks = true, LinkedEntities = linkedTasks };
    }
    
    private List<Link> CreateLinksForTask(HttpContext httpContext, Guid employeeId, Guid taskId, string fields = "")
    {
        var links = new List<Link>
        {
            new (_linkGenerator.GetUriByAction(httpContext, "GetTaskForEmployee", "Tasks", 
                    values: new { employeeId, taskId, fields })!,
                "self",
                "GET"),
            new (_linkGenerator.GetUriByAction(httpContext, "DeleteTaskForEmployee", "Tasks", 
                    values: new { employeeId, taskId })!,
                "delete_task",
                "DELETE"),
            new (_linkGenerator.GetUriByAction(httpContext, "UpdateTaskForEmployee", "Tasks", 
                    values: new { employeeId, taskId })!,
                "update_task",
                "PUT"),
            new (_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateTaskForEmployee", "Tasks", 
                    values: new { employeeId, taskId })!,
                "partially_update_task",
                "PATCH")
        };

        return links;
    }
    
    private LinkCollectionWrapper<Entity> CreateLinksForTasks(HttpContext httpContext, LinkCollectionWrapper<Entity> tasksWrapper)
    {
        tasksWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetTasksForEmployee", "Tasks", 
                values: new { })!,
            "self",
            "GET"));

        return tasksWrapper;
    }
}