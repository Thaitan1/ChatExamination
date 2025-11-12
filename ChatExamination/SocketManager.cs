namespace ChatExamination;
using SocketIOClient;

public class SocketManager
{
    private static SocketIO _client;
    private static readonly string Path = "/sys25d";
    public static List<string> messages;

    static SocketManager()
    {
        messages = [];
    }

    public static async Task Connect()
    {
        _client = new SocketIO("wss://api.leetcode.se", new SocketIOOptions
        {
            Path = Path
        });

        _client.On("message", response =>
        {
            string receivedMessage = response.GetValue<string>();
            Console.WriteLine($"Received message: {receivedMessage}");
        });

        _client.OnConnected += (sender, args) =>
        {
            Console.WriteLine("Connected");
        };
        
        _client.OnDisconnected += (sender, args) =>
        {
            Console.WriteLine("Disconnected");
        };
        
        await _client.ConnectAsync();

        await Task.Delay(2000);
        
        Console.WriteLine($"Connected: {_client.Connected}");
    }

    public static async Task SendMessage(string userName, string message)
    {
        await  _client.EmitAsync("message", $"{userName}: {message}");
        Console.WriteLine($"{userName} said: {message}");
    }
}