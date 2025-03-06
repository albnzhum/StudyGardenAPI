using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class User : IModel
{
    [Key, Required, NotNull] 
    public int ID { get; private set; }

    [Required, NotNull] 
    public string Username { get; private set; }

    [Required, NotNull] 
    public string HashedPassword { get; private set; }

    private User(string login, string hashedPassword)
    {
        Username = login;
        HashedPassword = hashedPassword;
    }

    private User(int id, string login, string hashedPassword)
    {
        ID = id;
        Username = login;
        HashedPassword = hashedPassword;
    }
    
    private User() {}

/* Реализация паттерна 'Фабричный метод' в виде статического метода
 * по созданию объекта и возврата ошибки
 */
    public static (User user, string error) Create(string login, string hashedPassword)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(login))
        {
            error = "Login cannot be null or empty.";
        }

        if (string.IsNullOrEmpty(hashedPassword))
        {
            error += "\nPassword cannot be null or empty.";
        }

        var user = new User(login, hashedPassword);

        return (user, error);
    }
}