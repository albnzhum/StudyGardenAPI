using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models;

public class Friend : IModel
{
    [Key, Required, NotNull]
    public int ID { get; private set; }
    
    [Required, NotNull]
    public int UserID { get; private set; }
    public User User { get; private set; }
    
    [Required, NotNull]
    public int FriendID { get; private set; }
    public User FriendFK { get; private set; }
    
    [Required, NotNull]
    public bool IsAccepted { get; private set; }

    private Friend(int userId, int friendId, bool isAccepted = false)
    {
        UserID = userId;
        FriendID = friendId;
        IsAccepted = isAccepted;
    }

    private Friend(int id, int userId, int friendId, bool isAccepted = false)
    {
        ID = id;
        UserID = userId;
        FriendID = friendId;
        IsAccepted = isAccepted;
    }
    
    private Friend() {}

    /* Реализация паттерна 'Фабричный метод' в виде статического метода
     * по созданию объекта и возврата ошибки
     */
    public static (Friend friend, string error) Create(int userId, int friendId, bool isAccepted)
    {
        var error = string.Empty;

        if (userId <= 0)
        {
            error = "User ID is invalid";
        }

        if (friendId <= 0)
        {
            error += "Friend ID is invalid";
        }
        
        var friend = new Friend(userId, friendId, isAccepted);
        
        return (friend, error);
    }
}