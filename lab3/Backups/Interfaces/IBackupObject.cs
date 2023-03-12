using Zio;
using Zio.FileSystems;

namespace Backups.Interfaces;

public interface IBackupObject
{
    public UPath RelativePath { get; set; }
    string GetObjectStringRelativePath();
    UPath GetObjectRelativeUPath();
    void Accept(IVisitor visitor, IRepository repository, ZipArchiveFileSystem zipArchiveFileSystem);
}