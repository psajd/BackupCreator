namespace Backups.Exceptions;

public class BackupException : Exception
{
    private BackupException()
    {
    }

    private BackupException(string message)
        : base(message)
    {
    }

    public static void ThrowIfNull(object o)
    {
        if (o == null)
        {
            throw NullArgumentExcetion("object is null");
        }
    }

    public static BackupException NullArgumentExcetion(string exc)
    {
        return new BackupException("Exc");
    }

    public static BackupException StorageAlreadyExist()
    {
        return new BackupException("Storage already exist");
    }

    public static BackupException RepoObjectAlreadyExist()
    {
        return new BackupException("Repo object already exist");
    }

    public static BackupException RestorePointAlreadyExist()
    {
        return new BackupException("Restore point already exist");
    }

    public static BackupException DifferentRepositories()
    {
        return new BackupException("Different repositories");
    }
}