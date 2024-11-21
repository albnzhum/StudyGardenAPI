namespace StudyGarden.API.Contracts.UserAchievement;

public record UserAchievementResponse(
    int ID,
    int UserID,
    int AchievementID,
    DateTime DateEarned);