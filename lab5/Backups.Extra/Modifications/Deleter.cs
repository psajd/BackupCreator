using Backups.Entities;
using Backups.Extra.Tools;
using Backups.Interfaces;

namespace Backups.Extra.Modifications;

public class Deleter : IDeleter
{
    public void Delete(IRepository repository, Backup backup, List<RestorePoint> restorePoints)
    {
        BackupExtraException.ThrowIfNull(repository);
        BackupExtraException.ThrowIfNull(backup);
        BackupExtraException.ThrowIfNull(restorePoints);

        for (int index = 0; index < restorePoints.Count; index++)
        {
            RestorePoint restorePoint = restorePoints[index];
            backup.DeleteRestorePoint(restorePoint);
            foreach (SingleStorage storage in restorePoint.Storage.GetSingleStorages())
            {
                repository.GetFileSystem().DeleteFile(storage.GetAbsoluteStoragePath());
            }
        }
    }
}