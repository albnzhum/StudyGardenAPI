namespace StudyGarden.Entities;

public class Friend
{
    public int ID { get; set; }
    
    public int UserID { get; set; }
    public int FriendID { get; set; }
    
    public bool IsAccepted { get; set; }
}