using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        // Respond back to the SvelteKit app
        await Clients.Caller.SendAsync("ReceiveMessage", "Okay, got it. Let me help you with that.");
    }
}
