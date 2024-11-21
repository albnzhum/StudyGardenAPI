namespace StudyGarden.API.Contracts.GardenRequests;

public record GardenResponse(
    int ID,
    int UserID,
    int TaskID,
    int PlantID,
    int GrowthStage,
    float PositionX,
    float PositionY,
    float PositionZ
    );