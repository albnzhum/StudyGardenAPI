namespace StudyGarden.API.Contracts.AchievementsRequests;

public record AchievementResponse(
    int ID,
    string Title,
    int PlantID);