using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Contracts;

public interface ITasksLinks
{
    LinkResponse TryGenerateLinks(IEnumerable<TasksDto> tasksDto, string fields, 
        Guid employeeId, HttpContext httpContext);
}