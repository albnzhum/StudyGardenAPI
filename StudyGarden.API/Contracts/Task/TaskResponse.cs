namespace StudyGarden.API.Contracts.Task;

public record TaskResponse(
    int ID,
    string Title,
    string? Description,
    int UserID,
    int PlantID,
    int? CategoryID,
    DateTime CreatedDate,
    DateTime? DueDate,
    int Status,
    int? Priority);