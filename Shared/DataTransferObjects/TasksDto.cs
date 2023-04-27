namespace Shared.DataTransferObjects;

public record TasksDto(Guid Id, string Title, string Description,
    DateTime CreatedAt, DateTime DueAt);