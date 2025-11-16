namespace ChatExamination;
using SocketIOClient;

/*public class Message
{
    public string Text { get; set; }
    public string Sender { get; set; }
    public string Time { get; set; }
}
*/
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
        
        Console.WriteLine("Type /exit to exit or type /history to see earlier messages");
        Console.WriteLine("Enter message: ");

        while (true)
        {
            string input =  Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input == "/history")
            {
                foreach (string msg in SocketManager.chatHistory)
                {
                    Console.WriteLine(msg);
                }
                continue;
            }
            
            if (input == "/exit")
                break;
                
            var message =  new Messages
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
        
    