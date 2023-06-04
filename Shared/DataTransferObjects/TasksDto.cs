namespace Shared.DataTransferObjects;

public record TasksDto(Guid Id, string Title, string Description,
    DateTimeOffset CreatedAt, DateTimeOffset DueAt, string State, string Priority);