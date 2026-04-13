using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Estate.ApiClient.Abstractions.legacy;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Estate.ApiClient.legacy.InventoryCards
{
    [InjectAsSingleton(typeof(IInventoryCardApiClient))]
    internal sealed class InventoryCardApiClient : BaseLegacyApiClient, IInventoryCardApiClient
    {
        public InventoryCardApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<InventoryCardApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<List<InventoryCardDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.NullOrEmpty())
            {
                return Task.FromResult(new List<InventoryCardDto>());
            }

            var uri = $"/InventoryCardApi/GetByBaseDocumentIds?firmId={firmId}&userId={userId}";
            
            return PostAsync<IReadOnlyCollection<long>, List<InventoryCardDto>>(uri,
                baseIds.ToDistinctReadOnlyCollection());
        }

        public Task<PaidSumDto[]> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.NullOrEmpty())
            {
                return Task.FromResult(Array.Empty<PaidSumDto>());
            }

            var uri = $"/InventoryCardApi/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
            
            return PostAsync<IReadOnlyCollection<long>, PaidSumDto[]>(uri, baseIds.ToDistinctReadOnlyCollection());
        }

        public Task<Dictionary<long, InventoryCardDto>> GetByPrimaryDocumentBaseIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> primaryDocumentBaseIds)
        {
            if (primaryDocumentBaseIds.NullOrEmpty())
            {
                return Task.FromResult(new Dictionary<long, InventoryCardDto>());
            }

            var uri = $"/InventoryCardApi/GetByPrimaryDocumentBaseIds?firmId={firmId}&userId={userId}";
            
            return PostAsync<IReadOnlyCollection<long>, Dictionary<long, InventoryCardDto>>(uri,
                primaryDocumentBaseIds.ToDistinctReadOnlyCollection());
        }

        public Task TaxProvideByFixedAssetInvestmentBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> fixedAssetBaseIds)
        {
            if (fixedAssetBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/InventoryCardApi/TaxProvideInventoryCard?firmId={firmId}&userId={userId}", 
                fixedAssetBaseIds);
        }
    }
}