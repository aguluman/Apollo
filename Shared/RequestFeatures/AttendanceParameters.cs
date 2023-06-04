using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures;

public class AttendanceParameters : RequestParameters
{
    public AttendanceParameters() => OrderBy = "clockIn";

    
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset MinClockIn { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset MaxClockIn { get; set; } = DateTimeOffset.MaxValue;

    public bool ValidClockInRange => MaxClockIn > MinClockIn;

    
    
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset MinClockOut { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset MaxClockOut { get; set; } = DateTimeOffset.MaxValue;

    public bool ValidClockOutRange => MaxClockOut > MinClockOut;

    
    
    public string? SearchTerm { get; set; }
    
    public string EmployeeName { get; set; }
    
    public string CompanyName { get; set; }
}