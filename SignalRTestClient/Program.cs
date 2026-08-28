using Microsoft.AspNetCore.SignalR.Client;

var token = Environment.GetEnvironmentVariable("SIGNALR_TOKEN");

if (string.IsNullOrWhiteSpace(token))
{
    Console.WriteLine("Token is missing.");
    return;
}

var connection = new HubConnectionBuilder()
    .WithUrl(
        "https://localhost:7247/hubs/chat",
        options =>
        {
            options.AccessTokenProvider = () =>
                Task.FromResult<string?>(token);
        })
    .WithAutomaticReconnect()
    .Build();
connection.Closed += async (error) =>
{
    Console.WriteLine($"Connection closed: {error?.Message}");

    await Task.Delay(2000);

    try
    {
        await connection.StartAsync();
        Console.WriteLine("Reconnected!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Reconnect failed: {ex.Message}");
    }
};

connection.On<object>("ReceiveMessage", message =>
{
    Console.WriteLine($"Received: {message}");
});
connection.On<Object>("ReceiveMessage", notification => Console.WriteLine($" Notification: {notification}"));

try
{
    await connection.StartAsync();

    Console.WriteLine("Connected to SignalR!");

    await connection.InvokeAsync("MarkMessagesAsRead", 1);

    Console.WriteLine("Messages marked as read.");
}
catch (Exception ex)
{
    Console.WriteLine($"Connection failed: {ex.Message}");
    return;
}

while (true)
{
    Console.Write("Message: ");

    var message = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(message))
        continue;

    try
    {
        await connection.InvokeAsync(
            "SendMessage",
            1,
            message);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}