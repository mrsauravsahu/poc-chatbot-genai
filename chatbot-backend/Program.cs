var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddSingleton((_) => {
    return new OpenAIClient(Environment.GetEnvironmentVariable("OPENAI_KEY"));
});

var app = builder.Build();

app.MapHub<ChatHub>("/chathub");

app.Run();
