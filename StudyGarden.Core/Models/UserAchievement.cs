using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class UserAchievement : IModel
{
    [Key, Required, NotNull]
    public int ID { get; private set; }
    
    [Required, NotNull]
    public int UserID { get; private set; }
    public User User { get; private set; }
    
    [Required, NotNull]
    public int AchievementID { get; private set; }
    public Achievement Achievement { get; private set; }
    
    [Required, NotNull]
    public DateTime DateEarned { get; private set; }

    private UserAchievement(int userId, int achievementId, DateTime dateEarned)
    {
        UserID = userId;
        AchievementID = achievementId;
        DateEarned = dateEarned;
    }

    private UserAchievement(int id, int userId, int achievementId, DateTime dateEarned)
    {
        ID = id;
        UserID = userId;
        AchievementID = achievementId;
        DateEarned = dateEarned;
    }
    
    private UserAchievement() {}

    /* Реализация паттерна 'Фабричный метод' в виде статического метода
     * по созданию объекта и возврата ошибки
     */
    public static (UserAchievement userAchievement, string error) Create(int userId, int achievementId,
        DateTime dateEarned)
    {
        var error = string.Empty;

        if (userId <= 0)
        {
            error += "\nUser ID is required";
        }

        if (achievementId <= 0)
        {
            error += "\nAchievement ID is required";
        }

        if (dateEarned == DateTime.MinValue)
        {
            error += "\nDate is reqiured";
        }

        var userAchievement = new UserAchievement(userId, achievementId, dateEarned);
        
        return (userAchievement, error);
    }
}