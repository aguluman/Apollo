using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Contracts;

public interface IAttendanceLinks
{
    //Todo : Add IAttendanceLinks
    LinkResponse TryGenerateLinks(IEnumerable<AttendanceDto> attendancesDto, string fields,
        Guid employeeId, HttpContext httpContext);
}