namespace ChatExamination;

public class Messages
{
    public string Time { get; set; }
    public string Sender { get; set; }
    public string Text { get; set; }

    public Messages() { }

    public Messages(string time, string sender, string text)
    {
        Time = time;
        Sender = sender;
        Text = text;
    }
}