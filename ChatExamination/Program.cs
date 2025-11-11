namespace ChatExamination;
using SocketIOClient;

class Message
{
    public string Text { get; set; }
    public string Sender { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Pick a username:");
        var userName = Console.ReadLine();

        while (userName == "")
        {
            Console.WriteLine("Please enter username:");
            userName = Console.ReadLine();
        }

        Console.WriteLine($"Your username is: {userName}");
    
        await SocketManager.Connect();

        while (true)
        {

        }
    }
}
        
    