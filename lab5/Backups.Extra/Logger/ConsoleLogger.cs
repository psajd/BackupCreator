namespace Backups.Extra.Logger;

public class ConsoleLogger : ILogger
{
    public void Log(string information)
    {
        Console.WriteLine(information);
    }
}