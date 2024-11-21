namespace StudyGarden.API.Contracts.UserCategory;

public record UserCategoryRequest(
    int UserID,
    int PlantTypeID,
    string Title);