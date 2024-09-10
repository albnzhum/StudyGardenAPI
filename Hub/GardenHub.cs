using Microsoft.AspNetCore.SignalR;

namespace StudyGarden.Hub;

public class GardenHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task NotifyGardenUpdate(int friendUserID)
    {
        await Clients.Group(friendUserID.ToString()).SendAsync("ReceiveGardenUpdate", friendUserID);
    }
}