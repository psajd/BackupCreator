using Backups.Algorithms;
using Backups.Configs;
using Backups.Entities;
using Backups.Extra.Logger;
using Backups.Extra.Modifications;
using Backups.Extra.Restore;
using Backups.Extra.Selection;
using Backups.Extra.Task;
using Backups.Extra.Tools;
using Backups.Interfaces;
using Backups.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace Backups.Extra.Test;

public class ExtraTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ExtraTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    private void CreateRestorePoint()
    {
        IRepository repository = new InMemoryRepository();
        repository.CreateFile("123.txt").Dispose();
        repository.CreateDirectory("321");

        var backupTask = new BackupExtraTask(
            new TaskConfig(
                new SplitStorageAlgorithm(),
                repository,
                "1234"),
            new ExtraConfig(
                new ConsoleLogger(),
                new Merger(),
                new Deleter(),
                new SelectByCount(1)));

        IBackupObject backupObject1 = new BackupFileObject("123.txt");
        IBackupObject backupObject2 = new BackupDirectoryObject("321");

        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        RestorePoint a = backupTask.CreateRestorePoint();

        string json = BackupExtraTask.SaveData(backupTask);
        var task = BackupExtraTask.LoadData(json);
        _testOutputHelper.WriteLine(json);
        Assert.NotEmpty(backupTask.Backup.RestorePoints);
    }
}