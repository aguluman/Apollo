using Contracts;
using Entities.LinkModels;
using Shared.DataTransferObjects;

namespace Apollo.Utility;

public class AttendanceLinks : IAttendanceLinks
{
    //Todo : Add AttendanceLinks
    public LinkResponse TryGenerateLinks(IEnumerable<AttendanceDto> attendancesDto, string fields, Guid employeeId, HttpContext httpContext)
    {
        throw new NotImplementedException();
    }
}