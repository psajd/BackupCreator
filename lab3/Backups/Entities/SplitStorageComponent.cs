using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<SingleStorage> _storages = new ();

    public SplitStorage(IRepository repository)
    {
        BackupException.ThrowIfNull(repository);
        Repository = repository;
    }

    public IRepository Repository { get; }

    public IReadOnlyCollection<SingleStorage> Storages => _storages;

    public void AddSingleStorage(SingleStorage storage)
    {
        BackupException.ThrowIfNull(storage);

        if (storage.Repository != Repository)
        {
            throw BackupException.DifferentRepositories();
        }

        BackupException.ThrowIfNull(storage);
        if (_storages.Contains(storage))
        {
            throw BackupException.StorageAlreadyExist();
        }

        _storages.Add(storage);
    }

    public void DeleteSingleStorage(SingleStorage storage)
    {
        BackupException.ThrowIfNull(storage);
        BackupException.ThrowIfNull(storage);
        _storages.Remove(storage);
    }

    public IReadOnlyCollection<SingleStorage> GetSingleStorages()
    {
        return _storages;
    }
}