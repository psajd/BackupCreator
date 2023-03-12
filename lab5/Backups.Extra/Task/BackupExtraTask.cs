using Backups.Configs;
using Backups.Entities;
using Backups.Extra.Tools;
using Backups.Facades;
using Newtonsoft.Json;

namespace Backups.Extra.Task;

public class BackupExtraTask : BackupTask
{
    private static readonly JsonSerializerSettings Settings = new ()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented,
    };

    public BackupExtraTask(TaskConfig taskConfig, ExtraConfig extraConfig)
        : base(taskConfig)
    {
        BackupExtraException.ThrowIfNull(extraConfig);
        ExtraConfig = extraConfig;
    }

    public ExtraConfig ExtraConfig { get; }

    public static string SaveData(BackupExtraTask task)
    {
        BackupExtraException.ThrowIfNull(task);
        return JsonConvert.SerializeObject(task, Settings);
    }

    public static BackupExtraTask LoadData(string jsonString)
    {
        return JsonConvert.DeserializeObject<BackupExtraTask>(jsonString, Settings);
    }

    public void Merge()
    {
        ExtraConfig.Logger.Log($"Merge");
        List<RestorePoint> restorePoints = ExtraConfig.SelectionAlgorithm.SelectPoints(Backup.RestorePoints.ToList());
        ExtraConfig.Merger.Merge(TaskConfig.Repository, Backup, restorePoints);
        ExtraConfig.Logger.Log($"Merge successfully");
    }

    public void Delete()
    {
        ExtraConfig.Logger.Log($"delete");
        List<RestorePoint> restorePoints = ExtraConfig.SelectionAlgorithm.SelectPoints(Backup.RestorePoints.ToList());
        ExtraConfig.Deleter.Delete(TaskConfig.Repository, Backup, restorePoints);
        ExtraConfig.Logger.Log($"delete successfully");
    }
}