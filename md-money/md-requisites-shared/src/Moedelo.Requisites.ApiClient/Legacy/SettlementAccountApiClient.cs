using System;
using System.Collections.Generic;
using System.Linq;
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
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ISettlementAccountApiClient))]
    internal sealed class SettlementAccountApiClient : BaseLegacyApiClient, ISettlementAccountApiClient
    {
        public SettlementAccountApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SettlementAccountApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<SettlementAccountDto[]> GetAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/SettlementAccount?firmId={firmId}&userId={userId}";
            return GetAsync<SettlementAccountDto[]>(uri);
        }

        public Task<SettlementAccountDto> GetByIdAsync(FirmId firmId, UserId userId, int settlementAccountId)
        {
            var uri = $"/SettlementAccount/{settlementAccountId}?firmId={firmId}&userId={userId}";
            return GetAsync<SettlementAccountDto>(uri);
        }

        public Task<SettlementAccountDto> GetPrimaryAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/SettlementAccount/Primary?firmId={firmId}&userId={userId}";
            return GetAsync<SettlementAccountDto>(uri);
        }

        public Task<SettlementAccountDto[]> GetByNumbersAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<string> numbers)
        {
            if (numbers?.Any() != true)
            {
                return Task.FromResult(Array.Empty<SettlementAccountDto>());
            }

            var uri = $"/SettlementAccount/ByNumbers?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<string>, SettlementAccountDto[]>(uri,
                numbers.ToDistinctReadOnlyCollection());
        }

        public Task<SettlementAccountDto[]> GetByIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<int> settlementAccountIds)
        {
            if (settlementAccountIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<SettlementAccountDto>());
            }

            var uri = $"/SettlementAccount/ByIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<int>, SettlementAccountDto[]>(uri,
                settlementAccountIds.ToDistinctReadOnlyCollection());
        }

        public Task<IReadOnlyDictionary<int, SettlementAccountDto[]>> GetByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken = default)
        {
            if (firmIds?.Any() != true)
            {
                return Task.FromResult((IReadOnlyDictionary<int, SettlementAccountDto[]>)new Dictionary<int, SettlementAccountDto[]>());
            }

            const string uri = "/SettlementAccount/ByFirms";

            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, SettlementAccountDto[]>>(uri,
                firmIds.ToDistinctReadOnlyCollection(), cancellationToken: cancellationToken);
        }

        public Task<SettlementAccountDto[]> GetWithDeletedAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/SettlementAccount/WithDeleted?firmId={firmId}&userId={userId}";
            return GetAsync<SettlementAccountDto[]>(uri);
        }

        public Task<SavedSettlementAccountDto> SaveAsync(FirmId firmId, UserId userId,
            SettlementAccountDto settlementAccount)
        {
            var uri = $"/SettlementAccount?firmId={firmId}&userId={userId}";
            return PostAsync<SettlementAccountDto, SavedSettlementAccountDto>(uri, settlementAccount);
        }
        
        public Task ArchiveAsync(FirmId firmId, UserId userId, int settlementAccountId)
        {
            var uri = $"/SettlementAccount/Archive/{settlementAccountId}?firmId={firmId}&userId={userId}";
            return PostAsync(uri);
        }

        public Task DearchiveAsync(FirmId firmId, UserId userId, int settlementAccountId)
        {
            var uri = $"/SettlementAccount/Dearchive/{settlementAccountId}?firmId={firmId}&userId={userId}";
            return PostAsync(uri);
        }
    }
}