using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

namespace Entities.LinkModels;

public record AttendanceLinkParameters(AttendanceParameters AttendanceParameters, HttpContext Context);