using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.Modifications;

public interface IDeleter
{
    public void Delete(IRepository repository, Backup backup, List<RestorePoint> restorePoints);
}