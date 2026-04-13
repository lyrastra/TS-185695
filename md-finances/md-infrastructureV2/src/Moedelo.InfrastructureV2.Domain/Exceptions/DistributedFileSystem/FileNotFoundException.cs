using System;

namespace Moedelo.InfrastructureV2.Domain.Exceptions.DistributedFileSystem;

public class FileNotFoundException : Exception
{
    public FileNotFoundException(Exception innerException)
        : base("File not found", innerException)
    {
    }
}