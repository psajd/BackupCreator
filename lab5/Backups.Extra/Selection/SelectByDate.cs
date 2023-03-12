using Backups.Entities;
using Backups.Extra.Tools;

namespace Backups.Extra.Selection;

public class SelectByDate : ISelectionAlgorithm
{
    public SelectByDate(DateTime dateTime)
    {
        DateTime = dateTime;
    }

    public DateTime DateTime { get; }

    public List<RestorePoint> SelectPoints(List<RestorePoint> restorePoints)
    {
        BackupExtraException.ThrowIfNull(restorePoints);
        var selectPoints = new List<RestorePoint>();
        foreach (RestorePoint restorePoint in restorePoints)
        {
            if (restorePoint.Time >= DateTime)
            {
                selectPoints.Add(restorePoint);
            }
        }

        return selectPoints;
    }
}