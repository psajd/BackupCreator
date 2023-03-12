using Backups.Exceptions;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class Visitor : IVisitor
{
    public void Visit(BackupDirectoryObject directoryObject, IRepository repository, ZipArchiveFileSystem zipArchiveFileSystem)
    {
        BackupException.ThrowIfNull(directoryObject);
        BackupException.ThrowIfNull(repository);
        BackupException.ThrowIfNull(zipArchiveFileSystem);

        foreach (UPath enumerateDirectory in
                 repository.GetFileSystem()
                     .EnumerateDirectories(
                         UPath.Combine(repository.GetAbsolutePath(), directoryObject.GetObjectRelativeUPath()), "*", SearchOption.TopDirectoryOnly))
        {
            UPath path = new UPath(Path.GetRelativePath(repository.GetAbsolutePath().FullName, enumerateDirectory.FullName)).ToAbsolute();
            zipArchiveFileSystem.CreateDirectory(path);
        }

        foreach (UPath enumerateFile in
                 repository.GetFileSystem()
                     .EnumerateFiles(
                         UPath.Combine(repository.GetAbsolutePath(), directoryObject.GetObjectRelativeUPath()), "*", SearchOption.AllDirectories))
        {
            UPath path = new UPath(Path.GetRelativePath(repository.GetAbsolutePath().FullName, enumerateFile.FullName)).ToAbsolute();
            repository.GetFileSystem().CopyFileCross(
                enumerateFile.ToAbsolute(),
                zipArchiveFileSystem,
                path,
                true);
        }
    }

    public void Visit(BackupFileObject fileObject, IRepository repository, ZipArchiveFileSystem zipArchiveFileSystem)
    {
        BackupException.ThrowIfNull(fileObject);
        BackupException.ThrowIfNull(repository);
        BackupException.ThrowIfNull(zipArchiveFileSystem);

        var path = UPath.Combine(repository.GetAbsolutePath(), fileObject.GetObjectRelativeUPath());
        repository.GetFileSystem().CopyFileCross(
            path.ToAbsolute(),
            zipArchiveFileSystem,
            fileObject.GetObjectRelativeUPath().ToAbsolute(),
            true);
    }
}