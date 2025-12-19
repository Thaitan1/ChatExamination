namespace ChatExamination;

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
        
        Console.WriteLine("Commands: /help , /exit , /history");
        Console.WriteLine("Enter message: ");

        while (true)
        {
            string time = DateTime.Now.ToString("yy-MM-dd HH:mm");
            string input =  Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input == "/help")
            {
                Console.WriteLine("Type /exit to exit or /history to see earlier messages");
                    continue;
            }

            if (input == "/history")
            {
                foreach (string msg in SocketManager.ChatHistory)
                {
                    Console.WriteLine(msg);
                }
                continue;
            }

            if (input == "/exit")
            {
                await SocketManager.Disconnect(userName);
                break;
            }

            var message = new Messages(time, userName, input);

            
            await SocketManager.SendMessage(message);
        }
        
        Console.WriteLine("Exiting program");
    }
}