using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.UploadFile
{
    public interface IReportFileClient : IDI
    {
        Task<FileDto> GetFileAsync(int fileId);
        Task<int> UploadFormalDocumentIncomingFileAsync(FileDto fileDto);
        Task UploadFormalDocumentOutgoingFileWithVisibleCrossAsync(FileDto fileDto, CancellationToken cancellationToken);
        Task<int> UploadNonFormalDocumentIncomingFileAsync(FileDto fileDto);
    }
}