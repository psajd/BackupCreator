using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Configs;

public class TaskConfig
{
    public TaskConfig(IAlgorithm algorithm, IRepository repository, string taskName)
    {
        Algorithm = algorithm ?? throw BackupException.NullArgumentExcetion("algorithm");
        Repository = repository ?? throw BackupException.NullArgumentExcetion("repository");
        TaskName = taskName ?? throw BackupException.NullArgumentExcetion("taskname");
    }

    public IAlgorithm Algorithm { get; }

    public IRepository Repository { get; }

    public string TaskName { get; }
}