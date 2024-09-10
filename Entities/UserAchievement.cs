namespace StudyGarden.Entities;

public class UserAchievement
{
    public int ID { get; set; }
    
    public int UserID { get; set; }
    
    public int AchievementID { get; set; }
    
    public DateTime DateEarned { get; set; }
}