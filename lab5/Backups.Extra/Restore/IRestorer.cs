using Backups.Entities;
using Zio;

namespace Backups.Extra.Restore;

public interface IRestorer
{
    public void RestoreToOriginal(RestorePoint restorePoint);

    public void RestoreToDifferent(RestorePoint restorePoint, UPath path);
}