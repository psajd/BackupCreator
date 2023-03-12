using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class SingleStorage : IStorage
{
    private readonly List<IBackupObject> _backupObjects = new ();
    private readonly Guid _guid;

    public SingleStorage(IRepository repository, Guid id, int number)
    {
        BackupException.ThrowIfNull(repository);
        BackupException.ThrowIfNull(id);

        StorageNumber = number;
        Repository = repository ?? throw BackupException.NullArgumentExcetion("repository");
        _guid = id;
    }

    public int StorageNumber { get; }

    public IRepository Repository { get; }

    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;

    public string GetAbsoluteStoragePath()
    {
        return Path.Combine(Repository.GetAbsolutePath().FullName, $"({StorageNumber.ToString()}) ") + _guid + ".zip";
    }

    public string GetRelativeStoragePath()
    {
        return Path.Combine("(1)", _guid.ToString()) + ".zip";
    }

    public void AddObject(IBackupObject backupObject)
    {
        BackupException.ThrowIfNull(backupObject);
        if (_backupObjects.Contains(backupObject))
        {
            throw BackupException.RepoObjectAlreadyExist();
        }

        _backupObjects.Add(backupObject);
    }

    public void DeleteObject(IBackupObject backupObject)
    {
        BackupException.ThrowIfNull(backupObject);
        _backupObjects.Remove(backupObject);
    }

    public IReadOnlyCollection<IBackupObject> GetObjects()
    {
        return _backupObjects;
    }

    public IReadOnlyCollection<SingleStorage> GetSingleStorages()
    {
        return new List<SingleStorage> { this };
    }
}