using Backups.Entities;
using Backups.Extra.Tools;

namespace Backups.Extra.Selection;

public class SelectHybrid : ISelectionAlgorithm
{
    public SelectHybrid(SelectByCount selectByCount, SelectByDate selectByDate)
    {
        SelectByCount = selectByCount;
        SelectByDate = selectByDate;
    }

    public SelectByCount SelectByCount { get; }
    public SelectByDate SelectByDate { get; }

    public List<RestorePoint> SelectPoints(List<RestorePoint> restorePoints)
    {
        BackupExtraException.ThrowIfNull(restorePoints);

        return SelectByDate.SelectPoints(SelectByCount.SelectPoints(restorePoints));
    }
}