namespace StudyGarden.API.Contracts.Plant;

public record PlantRequest(
    string Name,
    int PlantTypeID,
    bool isUnlocked);