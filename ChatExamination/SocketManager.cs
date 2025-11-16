namespace ChatExamination;
using SocketIOClient;

public class SocketManager
{
    private static SocketIO _client;
    private static readonly string Path = "/sys25d";
    public static List<string> chatHistory = new();

    public static async Task Connect(string userName)
    {
        _client = new SocketIO("wss://api.leetcode.se", new SocketIOOptions
        {
            Path = Path
        });

        _client.On("message", response =>
        {
            string receivedMessage = response.GetValue<string>();
            Console.WriteLine($"Received message: {receivedMessage}");
            chatHistory.Add(receivedMessage);
        });

        _client.On("userJoined", response =>
        {
            string newUser = response.GetValue<string>();
            Console.WriteLine($"{userName} Joined chat");
        });

        _client.On("userLeft", response =>
        {
            string newUser = response.GetValue<string>();
            Console.WriteLine($"{userName} Left chat");
        });

        _client.OnConnected += async (sender, args) =>
        {
            Console.WriteLine("Connected");
            await _client.EmitAsync("userJoined", userName);
        };
        
        _client.OnDisconnected += async (sender, args) =>
        {
            await _client.EmitAsync("userLeft", userName);
            Console.WriteLine("Disconnected");
        };
        
        await _client.ConnectAsync();

        await Task.Delay(2000);
        
        Console.WriteLine($"Connected: {_client.Connected}");
    }

    public static async Task SendMessage(Messages message)
    {
        string ownMessage = $"{message.Time}: You said: {message.Text}";
        chatHistory.Add(ownMessage);
        await _client.EmitAsync("message", message);
        Console.WriteLine($"{message.Time} {message.Sender} said: {message.Text}");
    }
}