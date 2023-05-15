using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures;

public class TasksParameters : RequestParameters
{
    public TasksParameters() => OrderBy = "date";
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime MinTime { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime MaxTime { get; set; } = DateTime.MaxValue;
    
    public bool ValidTimeRange => MaxTime > MinTime;

    public string? SearchTerm { get; set; }
    
}