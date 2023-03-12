using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IAlgorithm
{
    public IStorage CreateStorage(List<IBackupObject> repoObjects, IRepository repository, int backupNumber)
    {
        BackupException.ThrowIfNull(repository);
        var storage = new SplitStorage(repository);
        foreach (IBackupObject backupObject in repoObjects)
        {
            var singleStorage = new SingleStorage(repository, Guid.NewGuid(), backupNumber);
            singleStorage.AddObject(backupObject);
            storage.AddSingleStorage(singleStorage);
        }

        return storage;
    }
}