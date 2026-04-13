using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.MongoFiles;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.DistributedFileSystem;

public interface IFileStorageBase
{
    Task<byte[]> GetFileDataByIdAsync(string id,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<Stream> GetFileDataStreamByIdAsync(
        string id,
        CancellationToken cancellationToken,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<byte[]> GetFileDataByNameAsync(
        string fileName,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<MongoFileInfo> GetFileNameByIdAsync(
        string id,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<MongoFileInfoTable> GetFileNamesByPathPatternAsync(
        string pathPattern, 
        int take = 5, 
        int skip = 0,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<MongoFileInfoTable> GetAllFileNamesByPathPatternAsync(
        string pathPattern,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
    Task<MongoFileInfoTable> GetFileNamesByPathAsync(string path,
        int take = 5,
        int skip = 0,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
    Task<MongoFileInfoTable> GetAllFileNamesByPathAsync(string path,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<bool> HasDataByIdAsync(
        string id,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<string> SaveFileAsync(
        string fileName, 
        byte[] fileData,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task DeleteFileByIdAsync(
        string id,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
}