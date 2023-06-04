namespace Entities.Exceptions;

public class TasksNotFoundException : NotFoundException
{
    public TasksNotFoundException(Guid taskId) 
        : base($"Task with id: {taskId} doesn't exist in the database.")
    {
    }
}