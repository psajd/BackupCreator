using Zio;

namespace Backups.Interfaces;

public interface IRepository
{
    UPath GetAbsolutePath();

    IFileSystem GetFileSystem();
    Stream CreateFile(string fileName);
    void DeleteFile(string fileName);
    void CreateDirectory(string directoryName);
    void DeleteDirectory(string directoryName);
    Stream ReadFile(string fileName);
    void WriteArchive(IStorage storage);
}