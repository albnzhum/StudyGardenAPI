using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using StudyGarden.Core.Abstractions.Model;

namespace StudyGarden.Core.Models
{
    public class Task : IModel
    {
        [Key, Required, NotNull]
        public int ID { get; private set; }
        
        [Required, NotNull]
        public string Title { get; private set; }
        public string? Description { get; private set; }
        
        [Required, NotNull]
        public int UserID { get; private set; }
        public User User { get; private set; }
        
        [Required, NotNull]
        public int PlantID { get; private set; }
        public Plant Plant { get; private set; }
        public int? CategoryID { get; private set; }
        public UserCategory? Category { get; private set; }

        [Required, NotNull]
        public DateTime CreatedDate { get; private set; }
        public DateTime? DueDate { get; private set; }
        
        [Required, NotNull]
        public int Status { get; private set; }
        
        public int? Priority { get; private set; }

        private Task(string title, int userId, int plantId, DateTime createdDate, int status)
        {
            Title = title;
            UserID = userId;
            PlantID = plantId;
            CreatedDate = createdDate;
            Status = status;
        }

        private Task(int id, string title, int userId, int plantId, DateTime createdDate, int status)
        {
            ID = id;
            Title = title;
            UserID = userId;
            PlantID = plantId;
            CreatedDate = createdDate;
            Status = status;
        }
        
        private Task() {}

        private Task(string title, [Optional] string description, int userId, int plantId,
            [Optional] int? categoryId, DateTime createdDate, [Optional] DateTime? dueDate, int status, [Optional] int? priority)
        {
            Title = title;
            
            if (description != null) Description = description;
            
            UserID = userId;
            PlantID = plantId;
            
            if (categoryId != 0) CategoryID = categoryId;

            CreatedDate = createdDate;
            
            if (dueDate != null) DueDate = dueDate;

            Status = status;
            
            if (priority != 0) Priority = priority;
        }

        /* Реализация паттерна 'Фабричный метод' в виде статического метода
         * по созданию объекта и возврата ошибки
         */
        public static (Task task, string error) Create(string title, [Optional] string description, int userId,
            int plantId,
            [Optional] int? categoryId, DateTime createdDate, [Optional] DateTime? dueDate, int status, [Optional] int? priority)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title))
            {
                error = "Title is required";
            }

            if (userId <= 0)
            {
                error += "\nUser ID is required";
            }

            if (plantId <= 0)
            {
                error = "\nPlant ID is required";
            }

            if (createdDate == DateTime.MinValue)
            {
                error += "\nCreated Date is required";
            }

            if (status <= 0)
            {
                error += "\nStatus is required";
            }
            
            var task = new Task (title, description, userId, plantId, categoryId, createdDate, dueDate, status, priority);

            return (task, error);
        }
    }
}
