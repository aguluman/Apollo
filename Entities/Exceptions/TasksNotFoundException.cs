namespace Entities.Exceptions;

public class TasksNotFoundException : NotFoundException
{
    public TasksNotFoundException(Guid taskId)
        : base($"Task with id: {taskId} does not exist in the database.")
    {
    }
}