using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IAlgorithm
{
    public IStorage CreateStorage(List<IBackupObject> repoObjects, IRepository repository, int backupNumber)
    {
        BackupException.ThrowIfNull(repository);
        var storage = new SingleStorage(repository, Guid.NewGuid(), backupNumber);
        foreach (IBackupObject backupObject in repoObjects)
        {
            storage.AddObject(backupObject);
        }

        return storage;
    }
}