using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ICashApiClient))]
    internal sealed class CashApiClient : BaseLegacyApiClient, ICashApiClient
    {
        public CashApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CashApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  auditHeadersGetter,
                  settingRepository.Get("AccountingApiEndpoint"),
                  logger)
        {
        }

        public Task<CashDto> GetByIdAsync(UserId userId, FirmId firmId, long id)
        {
            var uri = $"/Cash?firmId={firmId}&userId={userId}&id={id}";
            return GetAsync<CashDto>(uri);
        }

        public Task<CashDto[]> GetAsync(UserId userId, FirmId firmId)
        {
            var uri = $"/Cash?firmId={firmId}&userId={userId}";
            return GetAsync<CashDto[]>(uri);
        }

        public Task SetOtherKontragent(FirmId firmId, UserId userId, IReadOnlyCollection<long> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return Task.CompletedTask;
            }
            
            var uri = $"/Cash/SetOtherKontragent?firmId={firmId}&userId={userId}";
            
            return PostAsync(uri, ids.ToDistinctReadOnlyCollection());
        }
    }
}