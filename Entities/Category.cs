namespace StudyGarden.Entities;

public class Category
{
    public int ID { get; set; }
    
    public int UserID { get; set; }
    
    public int PlantTypeID { get; set; }
    
    public string Title { get; set; }
}