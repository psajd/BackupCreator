using System.IO.Compression;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Repositories;

public class FileSystemRepository : IRepository, IDisposable
{
    public FileSystemRepository(UPath fullPath)
    {
        BackupException.ThrowIfNull(fullPath);
        FullPath = fullPath;
        FileSystem = new PhysicalFileSystem();
    }

    public UPath FullPath { get; }

    public IFileSystem FileSystem { get; }

    public UPath GetAbsolutePath()
    {
        return FullPath;
    }

    public IFileSystem GetFileSystem()
    {
        return FileSystem;
    }

    public Stream CreateFile(string fileName)
    {
        BackupException.ThrowIfNull(fileName);
        return FileSystem.CreateFile(UPath.Combine(FullPath.FullName, fileName));
    }

    public void DeleteFile(string fileName)
    {
        BackupException.ThrowIfNull(fileName);

        FileSystem.DeleteFile(UPath.Combine(FullPath.FullName, fileName));
    }

    public void CreateDirectory(string directoryName)
    {
        BackupException.ThrowIfNull(directoryName);

        FileSystem.CreateDirectory(UPath.Combine(FullPath.FullName, directoryName));
    }

    public void DeleteDirectory(string directoryName)
    {
        BackupException.ThrowIfNull(directoryName);

        FileSystem.DeleteDirectory(UPath.Combine(FullPath.FullName, directoryName), true);
    }

    public Stream ReadFile(string fileName)
    {
        BackupException.ThrowIfNull(fileName);

        return FileSystem.OpenFile(UPath.Combine(FullPath.FullName, fileName), FileMode.Open, FileAccess.ReadWrite);
    }

    public void WriteArchive(IStorage storage)
    {
        BackupException.ThrowIfNull(storage);

        IReadOnlyCollection<SingleStorage> storages = storage.GetSingleStorages();
        IVisitor visitor = new Visitor();

        foreach (SingleStorage singleStorage in storages)
        {
            string s = FileSystem.ConvertPathToInternal(singleStorage.GetAbsoluteStoragePath());
            using var zipToOpen = new FileStream(s, FileMode.OpenOrCreate);
            using var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);
            var zipArchiveFileSystem = new ZipArchiveFileSystem(archive);

            foreach (IBackupObject singleStorageBackupObject in singleStorage.BackupObjects)
            {
                singleStorageBackupObject.Accept(visitor, this, zipArchiveFileSystem);
            }
        }
    }

    public void Dispose()
    {
        FileSystem.Dispose();
    }
}