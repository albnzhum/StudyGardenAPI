namespace StudyGarden.Entities;

public class Plant
{
    public int ID { get; set; }
    public string Name { get; set; }
    
    public int PlantTypeID { get; set; }
    
    public bool IsUnlocked { get; set; }
}