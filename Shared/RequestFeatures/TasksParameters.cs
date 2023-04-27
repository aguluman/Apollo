namespace Shared.RequestFeatures;

public  class TasksParameters : RequestParameters
{
    public TasksParameters() => OrderBy = "date";
    
    public DateTime MinTime { get; set; }
    
    public DateTime MaxTime { get; set; } = DateTime.MaxValue;
    
    public bool ValidTimeRange => MaxTime > MinTime;

    public string? SearchTerm { get; set; }
    
}

