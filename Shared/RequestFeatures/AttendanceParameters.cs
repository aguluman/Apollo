using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures;

public class AttendanceParameters : RequestParameters
{
    public AttendanceParameters() => OrderBy = "position";
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime EndDate { get; set; } = DateTime.MaxValue;

    public bool ValidDateRange => EndDate > StartDate;
    
    public string? SearchTerm { get; set; }
}