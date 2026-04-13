using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerDocument;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IWorkerDocumentApiClient))]
    internal sealed class WorkerDocumentApiClient : BaseLegacyApiClient, IWorkerDocumentApiClient
    {
        public WorkerDocumentApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WorkerApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<int>> CreateDocumentAsync(FirmId firmId, UserId userId, int workerId, 
            int categoryId, HttpFileModel file, CancellationToken ct)
        {
            return UploadFileAsync<IReadOnlyCollection<int>>(
                $"/WorkerDocument?firmId={firmId}&userId={userId}&workerId={workerId}&categoryId={categoryId}", file,
                cancellationToken: ct);
        }
        
        public Task<string> UploadFileToStorageAsync(FirmId firmId, UserId userId, HttpFileModel file, 
            CancellationToken ct)
        {
            return UploadFileAsync<string>($"/WorkerDocument/UploadFileToStorage?firmId={firmId}&userId={userId}", file,
                cancellationToken: ct);
        }
        
        public Task<HttpFileModel> DownloadFileFromStorageAsync(FirmId firmId, UserId userId, 
            FileFromStorageRequestDto request, CancellationToken ct)
        {
            try
            {
                return DownloadFileAsync($"/WorkerDocument/DownloadFileFromStorage?firmId={firmId}&userId={userId}", 
                    request, cancellationToken: ct);
            }
            catch (HttpRequestResponseStatusException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }

        public Task DeleteFromStorageAsync(DeleteFromStorageRequestDto request, CancellationToken ct)
        {
            return PostAsync($"/WorkerDocument/DeleteFromStorage", request, cancellationToken: ct);
        }

        public Task<FileSizeLimitDto> GetFileSizeLimitAsync(FirmId firmId, UserId userId, CancellationToken ct)
        {
            return GetAsync<FileSizeLimitDto>($"/WorkerDocument/GetFileSizeLimit?firmId={firmId}&userId={userId}",
                cancellationToken: ct);
        }

        public Task<long> GetWorkerFilesSizeAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct)
        {
            return GetAsync<long>(
                $"/WorkerDocument/GetWorkerFilesSize?firmId={firmId}&userId={userId}&workerId={workerId}",
                cancellationToken: ct);
        }
        
        public Task SavePrivateDocumentsAsync(FirmId firmId, UserId userId, int workerId, 
            IReadOnlyCollection<WorkerDocumentInfoDto> documents, CancellationToken ct)
        {
            return PostAsync(
                $"/WorkerDocument/SavePrivateDocuments?firmId={firmId}&userId={userId}&workerId={workerId}", documents,
                cancellationToken: ct);
        }
    }
}
