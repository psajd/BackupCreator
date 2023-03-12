using Backups.Entities;
using Zio.FileSystems;

namespace Backups.Interfaces;

public interface IVisitor
{
    void Visit(BackupDirectoryObject directoryObject, IRepository repository, ZipArchiveFileSystem zipArchiveFileSystem);
    void Visit(BackupFileObject fileObject, IRepository repository, ZipArchiveFileSystem zipArchiveFileSystem);
}