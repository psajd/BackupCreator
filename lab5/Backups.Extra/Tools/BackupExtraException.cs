namespace Backups.Extra.Tools;

public class BackupExtraException : Exception
{
    public BackupExtraException()
    {
    }

    public BackupExtraException(string message)
        : base(message)
    {
    }

    public static void ThrowIfNull(object o)
    {
        if (o == null)
        {
            throw new BackupExtraException("Null reference exception");
        }
    }
}