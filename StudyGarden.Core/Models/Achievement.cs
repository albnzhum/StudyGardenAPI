using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class Achievement : IModel
{
    [Key, Required, NotNull]
    public int ID { get; private set; }
    
    [Required, NotNull]
    public string Title { get; private set; }
    
    [Required, NotNull]
    public int PlantID { get; private set; }
    
    public Plant Plant { get; private set; }

    private Achievement(string title, int plantId)
    {
        Title = title;
        PlantID = plantId;
    }

    private Achievement(int id, string title, int plantId)
    {
        ID = id;
        Title = title;
        PlantID = plantId;
    }

    /* Реализация паттерна 'Фабричный метод' в виде статического метода
     * по созданию объекта и возврата ошибки
     */
    public static (Achievement achievement, string error) Create(string title, int plantId)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title))
        {
            error += "Title is required.";
        }

        if (plantId <= 0)
        {
            error += "PlantID is required.";
        }
        
        var achievement = new Achievement(title, plantId);

        return (achievement, error);
    }
}