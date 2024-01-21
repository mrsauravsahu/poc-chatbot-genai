using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly OpenAIClient openAIClient;

    public ChatHub(OpenAIClient openAIClient){
        this.openAIClient = openAIClient;
    }

    public async Task SendMessage(string message)
    {
        // Respond back to the SvelteKit app
        var response = await openAIClient.GetAssistantResponse(message);
        await Clients.Caller.SendAsync("ReceiveMessage", response);
    }
}
