namespace ChatExamination;
using SocketIOClient;

public class Message
{
    public string Text { get; set; }
    public string Sender { get; set; }
    public string Time { get; set; }
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
    
        await SocketManager.Connect(userName);
        
        Console.WriteLine("Enter message or type /exit to exit program.");

        while (true)
        {
            string input =  Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input == "/history")
            {
                //Console.WriteLine(messages);
            }
            
            if (input == "/exit")
                break;
                
            var message =  new Message
            {
                Sender = userName,
                Text = input,
                Time = DateTime.Now.ToString("yy-MM-dd HH:mm")
            };
            
            await SocketManager.SendMessage(message);
        }
        
        Console.WriteLine("Exiting program");
    }
}
        
    