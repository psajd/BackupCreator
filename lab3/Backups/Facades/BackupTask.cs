using Backups.Configs;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Facades;

public class BackupTask
{
    private static int _backupNumber;
    private readonly List<IBackupObject> _backupObjects = new ();

    public BackupTask(TaskConfig taskConfig)
    {
        BackupException.ThrowIfNull(taskConfig);
        TaskConfig = taskConfig;
    }

    public TaskConfig TaskConfig { get; }

    public Backup Backup { get; } = new ();

    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;

    public void AddBackupObject(IBackupObject backupObject)
    {
        BackupException.ThrowIfNull(backupObject);
        if (BackupObjects.Contains(backupObject))
        {
            throw BackupException.RepoObjectAlreadyExist();
        }

        _backupObjects.Add(backupObject);
    }

    public void DeleteBackupObject(IBackupObject backupObject)
    {
        BackupException.ThrowIfNull(backupObject);
        _backupObjects.Remove(backupObject);
    }

    public RestorePoint CreateRestorePoint()
    {
        _backupNumber++;
        IStorage storage = TaskConfig.Algorithm.CreateStorage(_backupObjects, TaskConfig.Repository, _backupNumber);
        TaskConfig.Repository.WriteArchive(storage);
        var restorePoint = new RestorePoint(storage, _backupNumber);
        Backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }

    public void DeleteRestorePoint(RestorePoint restorePoint)
    {
        BackupException.ThrowIfNull(restorePoint);
        Backup.DeleteRestorePoint(restorePoint);
    }

    public void AddRestorePoint(IStorage storage, int number)
    {
        BackupException.ThrowIfNull(storage);
        Backup.AddRestorePoint(new RestorePoint(storage, number));
    }
}