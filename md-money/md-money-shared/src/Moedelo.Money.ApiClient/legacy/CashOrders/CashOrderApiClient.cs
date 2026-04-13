using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.legacy.CashOrders;
using Moedelo.Money.ApiClient.Abstractions.legacy.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Money.ApiClient.legacy.CashOrders
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

        public Task<FirmCashOrderDto[]> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<FirmCashOrderDto>());
            }

            var uri = $"/CashOrder/GetByBaseIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, FirmCashOrderDto[]>(uri,
                baseIds.ToDistinctReadOnlyCollection());
        }
    }
}