namespace StudyGarden.API.Contracts.UserCategory;

public record UserCategoryResponse(
    int ID,
    int UserID,
    int PlantTypeID,
    string Title);