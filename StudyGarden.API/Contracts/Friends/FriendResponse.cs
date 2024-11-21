namespace StudyGarden.API.Contracts.FriendsRequests;

public record FriendResponse(
    int ID,
    int UserID,
    int FriendID,
    bool IsAccepted);