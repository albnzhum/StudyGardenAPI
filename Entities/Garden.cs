namespace StudyGarden.Entities;

public class Garden
{
    public int ID { get; set; }
    
    public int UserID { get; set; }
    
    public int TaskID { get; set; }
    public int PlantID { get; set; }
    
    public int GrowthStage { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}