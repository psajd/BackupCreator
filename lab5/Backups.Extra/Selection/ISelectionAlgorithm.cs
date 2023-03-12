using Backups.Entities;

namespace Backups.Extra.Selection;

public interface ISelectionAlgorithm
{
    public List<RestorePoint> SelectPoints(List<RestorePoint> restorePoints);
}