using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerDocument;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IWorkerDocumentApiClient
    {
        Task<IReadOnlyCollection<int>> CreateDocumentAsync(FirmId firmId, UserId userId, int workerId, int categoryId,
            HttpFileModel file, CancellationToken ct);
        Task<string> UploadFileToStorageAsync(FirmId firmId, UserId userId, HttpFileModel file, CancellationToken ct);
        Task<HttpFileModel> DownloadFileFromStorageAsync(FirmId firmId, UserId userId, 
            FileFromStorageRequestDto request, CancellationToken ct);
        Task DeleteFromStorageAsync(DeleteFromStorageRequestDto request, CancellationToken ct);
        Task<FileSizeLimitDto> GetFileSizeLimitAsync(FirmId firmId, UserId userId, CancellationToken ct);
        Task<long> GetWorkerFilesSizeAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct);
        Task SavePrivateDocumentsAsync(FirmId firmId, UserId userId, int workerId, 
            IReadOnlyCollection<WorkerDocumentInfoDto> documents, CancellationToken ct);
    }
}
