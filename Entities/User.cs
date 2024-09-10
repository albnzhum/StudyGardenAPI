using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudyGarden.Entities;

public class User 
{
    [Key]
    public int ID { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
}