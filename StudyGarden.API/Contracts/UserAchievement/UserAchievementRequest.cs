namespace StudyGarden.API.Contracts.UserAchievement;

public record UserAchievementRequest(
    int UserID,
    int AchievementID,
    DateTime DateEarned);