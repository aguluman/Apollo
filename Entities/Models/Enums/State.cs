namespace Entities.Models.Enums;

public enum State
{
    NotStarted,
    InProgress,
    Paused,
    Completed,
    TimeElapsedAndStillPending
}

/*public class StateDto
{
    public StateDto(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}*/