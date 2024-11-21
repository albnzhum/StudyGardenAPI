namespace StudyGarden.API.Contracts.FriendsRequests;

public record FriendRequest(
    int UserID,
    int FriendID,
    bool IsAccepted);