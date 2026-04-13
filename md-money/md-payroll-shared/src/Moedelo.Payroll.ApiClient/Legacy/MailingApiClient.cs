using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Mailing;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IMailingApiClient))]
    internal sealed class MailingApiClient : BaseLegacyApiClient, IMailingApiClient
    {
        public MailingApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MrotApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<int>> GetIdsByWorkerIdAsync(int firmId, int userId, int workerId,
            CancellationToken token = default)
        {
            return GetAsync<IReadOnlyCollection<int>>("/Mailings/IdsByWorkerId", new { firmId, userId, workerId },
                cancellationToken: token);
        }

        public Task SetWorkerMailingsAsync(int firmId, int userId, SetWorkerMailingsDto dto, CancellationToken token = default)
        {
            return PostAsync($"/Mailings/SetWorkerMailings?firmId={firmId}&userId={userId}", dto, cancellationToken: token);
        }

        public Task DeleteFromActiveMailingByWorkerIdAsync(int firmId, int userId, int workerId, CancellationToken token)
        {
            return PostAsync(
                $"/Mailings/FromActiveMailingByWorkerId?firmId={firmId}&userId={userId}&workerId={workerId}",
                cancellationToken: token);
        }
    }
}
