namespace StudyGarden.API.Contracts.Plant;

public record PlantResponse(
    int ID,
    string Name,
    int PlantTypeID,
    bool isUnlocked);