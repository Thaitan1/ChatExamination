namespace ChatExamination;

public class Messages
{
    public string Text { get; set; }
    public string Sender { get; set; }
    public string Time { get; set; }

    public Messages(string text, string sender, string time)
    {
        Text = text;
        Sender = sender;
        Time = time;
    }
}