using Backups.Entities;

namespace Backups.Interfaces;

public interface IStorage
{
    IReadOnlyCollection<SingleStorage> GetSingleStorages();
}