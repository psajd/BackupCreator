using Backups.Entities;
using Backups.Extra.Tools;

namespace Backups.Extra.Selection;

public class SelectByCount : ISelectionAlgorithm
{
    public SelectByCount(int value)
    {
        if (value < 0)
        {
            throw new BackupExtraException();
        }

        Value = value;
    }

    public int Value { get; }

    public List<RestorePoint> SelectPoints(List<RestorePoint> restorePoints)
    {
        BackupExtraException.ThrowIfNull(restorePoints);

        var list = new List<RestorePoint>();
        for (int i = restorePoints.Count - Value; i < restorePoints.Count; i++)
        {
            list.Add(restorePoints[i]);
        }

        return list;
    }
}