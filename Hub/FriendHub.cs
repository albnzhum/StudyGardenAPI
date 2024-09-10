using Microsoft.AspNetCore.SignalR;

namespace StudyGarden.Hub;

public class FriendHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task NotifyFriendRequestAccepted(int userId)
    {
        await Clients.User(userId.ToString()).SendAsync("ReceiveFriendRequestAccepted", "Your friend request has been accepted!");
    }

    public async Task NotifyFriendRequestDecline(int userId)
    {
        await Clients.User(userId.ToString())
            .SendAsync("ReceiveFriendRequestDecline", "Your friend has been declined!");
    }
}