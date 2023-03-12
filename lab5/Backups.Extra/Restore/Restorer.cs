using System.IO.Compression;
using Backups.Entities;
using Backups.Extra.Tools;
using Backups.Interfaces;
using Zio;

namespace Backups.Extra.Restore;

public class Restorer
{
    public Restorer(IRepository repository)
    {
        Repository = repository ?? throw new NullReferenceException();
    }

    public IRepository Repository { get; }

    public void RestoreToOriginal(RestorePoint restorePoint)
    {
        BackupExtraException.ThrowIfNull(restorePoint);

        RestoreToDifferent(restorePoint, Repository.GetAbsolutePath());
    }

    public void RestoreToDifferent(RestorePoint restorePoint, UPath path)
    {
        BackupExtraException.ThrowIfNull(restorePoint);

        foreach (SingleStorage storage in restorePoint.Storage.GetSingleStorages())
        {
            using ZipArchive archive = ZipFile.OpenRead(Repository.GetFileSystem().ConvertPathToInternal(storage.GetAbsoluteStoragePath()));
            foreach (ZipArchiveEntry zipArchiveEntry in archive.Entries)
            {
                zipArchiveEntry
                    .ExtractToFile(
                        Repository.GetFileSystem().ConvertPathToInternal(
                            UPath.Combine(
                                path,
                                zipArchiveEntry.Name)), true);
            }
        }
    }
}