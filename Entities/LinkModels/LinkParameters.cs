using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

namespace Entities.LinkModels;

public record LinkParameters(EmployeeParameters? EmployeeParameters, TasksParameters? TasksParameters,HttpContext Context);