using Backups.Algorithms;
using Backups.Configs;
using Backups.Entities;
using Backups.Facades;
using Backups.Interfaces;
using Backups.Repositories;
using Xunit;

namespace Backups.Test;

public class BackupTest
{
    [Fact]
    private void SecondTest()
    {
        IRepository repository = new InMemoryRepository();
        repository.CreateFile("123.txt").Dispose();
        repository.CreateDirectory("321");

        var backupTask = new BackupTask(new TaskConfig(
            new SplitStorageAlgorithm(),
            repository,
            "1234"));

        IBackupObject backupObject1 = new BackupFileObject("123.txt");
        IBackupObject backupObject2 = new BackupDirectoryObject("321");

        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.CreateRestorePoint();

        backupTask.DeleteBackupObject(backupObject1);
        backupTask.CreateRestorePoint();

        Assert.Equal(2, backupTask.Backup.RestorePoints.Count);
        Assert.Equal(3, backupTask.Backup.RestorePoints.SelectMany(restorePoint => restorePoint.Storage.GetSingleStorages()).ToList().Count);
    }
}