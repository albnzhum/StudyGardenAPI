namespace StudyGarden.Entities
{
    public class Task
    {
        public int ID { get; set; }

        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int PlantID { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
    }
}
