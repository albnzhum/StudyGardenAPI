using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class PlantType : IModel
{
    [Key, Required, NotNull]
    public int ID { get; private set; }
    
    [Required, NotNull]
    public string Name { get; private set; }

    private PlantType(int id, string name)
    {
        ID = id;
        Name = name;
    }

    private PlantType(string name)
    {
        Name = name;
    }

    /* Реализация паттерна 'Фабричный метод' в виде статического метода
     * по созданию объекта и возврата ошибки
     */
    public static (PlantType plantType, string error) Create(string name)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name))
        {
            error = "Name is required";
        }

        var plantType = new PlantType(name);
        
        return (plantType, error);
    }
}