using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ICashOrderApiClient))]
    internal sealed class CashOrderApiClient : BaseLegacyApiClient, ICashOrderApiClient
    {
        public CashOrderApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CashOrderApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }

            var uri = $"/CashOrder/DeleteOrders?firmId={firmId}&userId={userId}";
            return PostAsync(uri, baseIds);
        }

        public Task<List<FirmCashOrderDto>> GetByBaseIds(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<FirmCashOrderDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<FirmCashOrderDto>>($"/CashOrder/GetByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task ProvideAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count == 0)
            {
                return Task.CompletedTask;
            }

            var uri = $"/CashOrder/Provide?firmId={firmId}&userId={userId}";
            return PostAsync(uri, documentBaseIds);
        }
    }
}