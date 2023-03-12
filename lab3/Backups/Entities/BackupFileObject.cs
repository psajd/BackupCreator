﻿using Backups.Exceptions;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class BackupFileObject : IBackupObject
{
    public BackupFileObject(string path)
    {
        RelativePath = path;
    }

    public UPath RelativePath { get; set; }

    public string GetObjectStringRelativePath()
    {
        return RelativePath.FullName;
    }

    public UPath GetObjectRelativeUPath()
    {
        return RelativePath;
    }

    public void Accept(IVisitor visitor, IRepository repository, ZipArchiveFileSystem zipArchiveFileSystem)
    {
        BackupException.ThrowIfNull(repository);

        BackupException.ThrowIfNull(visitor);

        BackupException.ThrowIfNull(zipArchiveFileSystem);
        visitor.Visit(this, repository, zipArchiveFileSystem);
    }
}