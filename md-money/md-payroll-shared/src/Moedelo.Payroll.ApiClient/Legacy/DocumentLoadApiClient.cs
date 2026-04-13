using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IDocumentLoadApiClient))]
    internal class DocumentLoadApiClient : BaseLegacyApiClient, IDocumentLoadApiClient
    {
        public DocumentLoadApiClient(
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

        public Task<HttpFileModel> DownloadJobStatementAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct)
        {
            return DownloadFileAsync($"/DocumentLoad/DownloadJobStatement?firmId={firmId}&userId={userId}&workerId={workerId}", cancellationToken: ct);
        }

        public Task<HttpFileModel> DownloadPersonalOrderAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct)
        {
            return DownloadFileAsync($"/DocumentLoad/DownloadPersonalOrder?firmId={firmId}&userId={userId}&workerId={workerId}", cancellationToken: ct);
        }

        public Task<HttpFileModel> DownloadWorkContractAsync(FirmId firmId, UserId userId, int workerId, CancellationToken ct)
        {
            return DownloadFileAsync($"/DocumentLoad/DownloadWorkContract?firmId={firmId}&userId={userId}&workerId={workerId}", cancellationToken: ct);
        }
    }
}
