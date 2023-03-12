using Backups.Exceptions;

namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints = new ();

    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;

    public int BackupId { get; }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        BackupException.ThrowIfNull(restorePoint);
        if (_restorePoints.Contains(restorePoint))
        {
            BackupException.RestorePointAlreadyExist();
        }

        _restorePoints.Add(restorePoint);
    }

    public void DeleteRestorePoint(RestorePoint restorePoint)
    {
        BackupException.ThrowIfNull(restorePoint);
        _restorePoints.Add(restorePoint);
    }
}