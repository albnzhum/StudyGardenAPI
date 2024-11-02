using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class Garden : IModel
{
    [Key, Required, NotNull]
    public int ID { get; private set; }
    
    [Required, NotNull]
    public int UserID { get; private set; }
    public User User { get; private set; }
    
    [Required, NotNull]
    public int TaskID { get; private set; }
    public Task Task { get; private set; }
    
    [Required, NotNull]
    public int PlantID { get; private set; }
    public Plant Plant { get; private set; }
    
    [Required, NotNull]
    public int GrowthStage { get; private set; }
    
    public float PositionX { get; private set; }
    
    [Required, NotNull]
    public float PositionY { get; private set; }
    
    [Required, NotNull]
    public float PositionZ { get; private set; }
    
    private Garden(int userId, int taskId, int plantId, int growthStage, 
        float positionX, float positionY, float positionZ)
    {
        UserID = userId;
        TaskID = taskId;
        PlantID = plantId;
        GrowthStage = growthStage;
        PositionX = positionX;
        PositionY = positionY;
        PositionZ = positionZ;
    }
    
    private Garden(int id, int userId, int taskId, int plantId, int growthStage, 
        float positionX, float positionY, float positionZ)
    {
        ID = id;
        UserID = userId;
        TaskID = taskId;
        PlantID = plantId;
        GrowthStage = growthStage;
        PositionX = positionX;
        PositionY = positionY;
        PositionZ = positionZ;
    }

    /* Реализация паттерна 'Фабричный метод' в виде статического метода
     * по созданию объекта и возврата ошибки
     */
    public static (Garden garden, string error) Create(int userId, int taskId, int plantId, int growthStage,
        float positionX, float positionY, float positionZ)
    {
        var error = string.Empty;
        
        if (userId <= 0)
        {
            error = "User ID is invalid";
        }

        if (taskId <= 0)
        {
            error += "Task ID in invalid";
        }

        if (plantId <= 0)
        {
            error += "Plant ID in invalid";
        }

        if (growthStage <= 0)
        {
            error += "Growth Stage in invalid";
        }

        var garden = new Garden(userId, taskId, plantId, growthStage, positionX, positionY, positionZ);

        return (garden, error);
    }
}