using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class Plant : IModel
{
    [Key, Required, NotNull]
    public int ID { get; private set; }
    
    [Required, NotNull]
    public string Name { get; private set; }
    
    [Required, NotNull]
    public int PlantTypeID { get; private set; }
    
    public PlantType PlantType { get; private set; }
    
    [Required, NotNull]
    public bool IsUnlocked { get; private set; }

    private Plant(int id, string name, int plantTypeID, bool isUnlocked = false)
    {
        ID = id;
        Name = name;
        PlantTypeID = plantTypeID;
        IsUnlocked = isUnlocked;
    }

    private Plant(string name, int plantTypeId, bool isUnlocked = false)
    {
        Name = name;
        PlantTypeID = plantTypeId;
        IsUnlocked = isUnlocked;
    }

    /* Реализация паттерна 'Фабричный метод' в виде статического метода
     * по созданию объекта и возврата ошибки
     */
    public static (Plant plant, string error) Create(string name, int plantTypeId, bool isUnlocked = false)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name))
        {
            error = "Name can't be empty";
        }

        if (plantTypeId <= 0)
        {
            error = "PlantTypeId can't be negative";
        }
        
        var plant = new Plant(name, plantTypeId, isUnlocked);
        
        return (plant, error);
    }
}