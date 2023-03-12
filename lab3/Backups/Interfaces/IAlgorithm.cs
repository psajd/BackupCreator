namespace Backups.Interfaces;

public interface IAlgorithm
{
    IStorage CreateStorage(List<IBackupObject> repoObjects, IRepository repository, int backupNumber);
}