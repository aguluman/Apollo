using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures;

public class TasksParameters : RequestParameters
{
    public TasksParameters() => OrderBy = "date";
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTimeOffset MinTime { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTimeOffset MaxTime { get; set; } = DateTimeOffset.MaxValue;
    
    public bool ValidTimeRange => MaxTime > MinTime;

    public string? SearchTerm { get; set; }
    
}