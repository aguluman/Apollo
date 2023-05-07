using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

namespace Entities.LinkModels;

public record TasksLinkParameters(TasksParameters TasksParameters,HttpContext Context);