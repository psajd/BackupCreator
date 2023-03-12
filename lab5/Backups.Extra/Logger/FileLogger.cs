using System.Text;

namespace Backups.Extra.Logger;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger()
    {
    }

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public string FilePath => _filePath;

    public void Log(string information)
    {
        FileStream fstream = File.OpenWrite(FilePath);
        fstream.Write(Encoding.Default.GetBytes(information));
        fstream.Dispose();
    }
}