using Shared.RequestFeatures;
using Microsoft.AspNetCore.Http;


namespace Entities.LinkModels;

public record TasksLinkParameters(TasksParameters TasksParameters, HttpContext Context);
