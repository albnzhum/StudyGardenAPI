namespace StudyGarden.API.Contracts.GardenRequests;

public record GardenRequest(
    int UserID,
    int TaskID,
    int PlantID,
    int GrowthStage,
    float PositionX,
    float PositionY,
    float PositionZ);