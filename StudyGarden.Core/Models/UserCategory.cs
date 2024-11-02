using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class UserCategory : IModel
{
    [Key, Required, NotNull]
    public int ID { get; private set; }
    
    [Required, NotNull]
    public int UserID { get;  private set; }
    public User User { get; private set; }
    
    [Required, NotNull]
    public int PlantTypeID { get; private set; }
    public PlantType PlantType { get; private set; }
    
    [Required, NotNull]
    public string Title { get; private set; }

    private UserCategory(int userId, int plantTypeId, string title)
    {
        UserID = userId;
        PlantTypeID = plantTypeId;
        Title = title;
    }

    private UserCategory(int id, int userId, int plantTypeId, string title)
    {
        ID = id;
        UserID = userId;
        PlantTypeID = plantTypeId;
        Title = title;
    }

    /* Реализация паттерна 'Фабричный метод' в виде статического метода
     * по созданию объекта и возврата ошибки
     */
    public static (UserCategory userCategory, string error) Create(int userId, int plantTypeId, string title)
    {
        var error = string.Empty;

        if (userId <= 0)
        {
            error = "User ID is required";
        }

        if (plantTypeId <= 0)
        {
            error += "\nPlantTypeId is required";
        }

        if (string.IsNullOrEmpty(title))
        {
            error += "\nTitle is required";
        }
        
        var userCategory = new UserCategory(userId, plantTypeId, title);

        return (userCategory, error);
    }
    
}