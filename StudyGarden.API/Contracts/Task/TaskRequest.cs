namespace StudyGarden.API.Contracts.Task;

public record TaskRequest(
    string Title,
    string? Description,
    int UserID,
    int PlantID,
    int? CategoryID,
    DateTime CreatedDate,
    DateTime? DueDate,
    int Status,
    int? Priority);