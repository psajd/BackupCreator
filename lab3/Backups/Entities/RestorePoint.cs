using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class RestorePoint
{
    public RestorePoint(IStorage storage, int restorePointNumber)
    {
        BackupException.ThrowIfNull(storage);
        RestorePointNumber = restorePointNumber;
        Storage = storage;
        Time = DateTime.Now;
    }

    public DateTime Time { get; }

    public int RestorePointNumber { get; }

    public IStorage Storage { get; set; }
}