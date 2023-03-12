using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.Modifications;

public interface IMerger
{
    public void Merge(IRepository repository, Backup backup, List<RestorePoint> restorePoints);
}