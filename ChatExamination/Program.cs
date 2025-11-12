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

        while (string.IsNullOrWhiteSpace(userName))
        {
            Console.WriteLine("Please enter username:");
            userName = Console.ReadLine();
        }

        Console.WriteLine($"Your username is: {userName}");
    
        await SocketManager.Connect();
        
        Console.WriteLine("Enter message or type /exit to exit program.");

        while (true)
        {
            string input =  Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input == "/history")
            {
                
            }
            
            if (input == "/exit")
                break;
                
            var message =  new Message
            {
                Sender = userName,
                Text = input
            };
            
            await SocketManager.SendMessage(userName, input);
        }
        
        Console.WriteLine("Exiting program");
    }
}
        
    