namespace ChatExamination;
using SocketIOClient;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class SocketManager
{
    private static SocketIO _client;
    private static readonly string Path = "/sys25d";
    public static List<string> ChatHistory = new();

    public static async Task Connect(string userName)
    {
        _client = new SocketIO("wss://api.leetcode.se", new SocketIOOptions
        {
            Path = Path
        });
        
        _client.On("message", response =>
        {
            try
            {
                var receivedMessage = response.GetValue<List<Messages>>();

                foreach (var message in receivedMessage)
                {
                    Console.WriteLine($"{message.Time} {message.Sender}: {message.Text}");
                    ChatHistory.Add($"{message.Time} {message.Sender}: {message.Text}");
                }
            }
            catch (Exception ex)
            {
                string text = response.GetValue<string>();
                string time = DateTime.Now.ToString("yy-MM-dd HH:mm");
                Console.WriteLine($"{time}: {text}");
                ChatHistory.Add($"{time}: {text}");
            }
        });
        
        _client.On("Joined", response =>
        {
            string newUser = response.GetValue<string>();
            Console.WriteLine($"{newUser} joined chat");
        });
        
        _client.OnConnected += async (sender, args) =>
        {
            Console.WriteLine("Connected");
            await _client.EmitAsync("Joined", userName);
        };

        _client.OnDisconnected += async (sender, args) =>
        {
            Console.WriteLine("Disconnected");
            await _client.EmitAsync("Left", userName);
        };

        await _client.ConnectAsync();

        await Task.Delay(2000);

        Console.WriteLine($"Connected: {_client.Connected}");
    }
    
    public static async Task SendMessage(Messages message)
    {
        string ownMessage = $"{message.Time}: You said: {message.Text}";
        ChatHistory.Add(ownMessage);
        await _client.EmitAsync("message", message);
        Console.WriteLine($"{message.Time} {message.Sender} said: {message.Text}");
    }
    
    public static async Task Disconnect(string userName)
    {
        await _client.EmitAsync("Left", userName);
        await _client.DisconnectAsync();
    }
}