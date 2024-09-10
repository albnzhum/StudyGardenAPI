using Microsoft.AspNetCore.SignalR;

namespace StudyGarden.Hub;

public class TaskHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task NotifyGardenUpdate(int friendUserID)
    {
        await Clients.Group(friendUserID.ToString()).SendAsync("ReceiveGardenUpdate", friendUserID);
    }
}