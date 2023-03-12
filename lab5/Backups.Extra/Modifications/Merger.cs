using Backups.Entities;
using Backups.Extra.Task;
using Backups.Extra.Tools;
using Backups.Interfaces;
using Zio;

namespace Backups.Extra.Modifications;

public class Merger : IMerger
{
    public void Merge(IRepository repository, Backup backup, List<RestorePoint> restorePoints)
    {
        BackupExtraException.ThrowIfNull(repository);
        BackupExtraException.ThrowIfNull(backup);
        BackupExtraException.ThrowIfNull(restorePoints);

        IFileSystem fs = repository.GetFileSystem();

        for (int i = 0; i < restorePoints.Count - 1; i++)
        {
            RestorePoint currentRestorePoint = restorePoints[i];
            RestorePoint nextRestorePoint = restorePoints[i + 1];
            foreach (SingleStorage singleStorage1 in currentRestorePoint.Storage.GetSingleStorages())
            {
                foreach (SingleStorage singleStorage2 in nextRestorePoint.Storage.GetSingleStorages())
                {
                    if (!singleStorage1.BackupObjects.Except(singleStorage2.BackupObjects).Any()) continue;
                    var storage = new SplitStorage(repository);
                    storage.AddSingleStorage(singleStorage1);
                    storage.AddSingleStorage(singleStorage2);
                    nextRestorePoint.Storage = storage;
                }
            }
        }
    }
}