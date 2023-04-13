namespace MyChat.Server.BL.Helpers;

public class ResultObject
{
    public int Id { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; }

    public object Extera { get; set; }
}