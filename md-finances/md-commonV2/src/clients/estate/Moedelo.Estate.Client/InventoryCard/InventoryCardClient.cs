using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Estate.Client.InventoryCard.Dto;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Estate.Client.InventoryCard
{
    [InjectAsSingleton]
    public class InventoryCardClient : BaseApiClient, IInventoryCardClient
    {
        private readonly SettingValue apiEndpoint;

        public InventoryCardClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<InventoryCardDto>> GetByDocumentBaseIds(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<InventoryCardDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<InventoryCardDto>>(
                $"/InventoryCardApi/GetByBaseDocumentIds?firmId={firmId}&userId={userId}",
                documentBaseIds);
        }

        public Task<InventoryCardDto> GetById(int firmId, int userId, long id)
        {
            return GetAsync<InventoryCardDto>(
                "/InventoryCardApi/GetById",
                new { firmId, userId, id });
        }

        public Task<InventoryCardDto> GetByFixedAssetBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<InventoryCardDto>(
                "/InventoryCardApi/GetByFixedAssetBaseId", 
                new { firmId, userId, documentBaseId });
        }

        public Task UpdateFromBalancesAsync(int firmId, int userId,
            IReadOnlyCollection<InventoryCardFromBalancesSaveDto> balances)
        {
            if (balances == null)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync(
                $"/InventoryCardApi/SaveInventoryCardBalances?firmId={firmId}&userId={userId}", 
                balances);
        }

        public Task<List<InventoryCardDto>> GetAmortizableInventoryCardsAsync(int firmId, int userId)
        {
            return PostAsync<List<InventoryCardDto>>($"/InventoryCardApi/GetAmortizableInventoryCards?firmId={firmId}&userId={userId}");
        }

        public Task<InventoryCardNotPayedInBalancesDto> GetNotPayedInBalancesAsync(int firmId, int userId, int kontragentId, long baseId)
        {
            return GetAsync<InventoryCardNotPayedInBalancesDto>(
                "/InventoryCardApi/GetNotPayedInBalances", 
                new { firmId, userId, kontragentId, baseId });
        }

        public Task TaxProvideByFixedAssetInvestmentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> fixedAssetBaseIds)
        {
            if (fixedAssetBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/InventoryCardApi/TaxProvideInventoryCard?firmId={firmId}&userId={userId}", 
                fixedAssetBaseIds);
        }

        public Task SaveAccAmortizationAsync(int firmId, int userId, IReadOnlyCollection<AccAmortizationUpdateRequestDto> saveRequest)
        {
            if (saveRequest?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync(
                $"/InventoryCardApi/SaveAccAmortization?firmId={firmId}&userId={userId}", 
                saveRequest);
        }

        public Task SaveTaxAmortizationAsync(int firmId, int userId, IReadOnlyCollection<TaxAmortizationUpdateRequestDto> saveRequest)
        {
            if (saveRequest?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync(
                $"/InventoryCardApi/SaveTaxAmortization?firmId={firmId}&userId={userId}", 
                saveRequest);
        }

        public Task<InventoryCardFromBizToAccTransferResponseDto> CreateInventoryCardFromBizToAccTransferAsync(int firmId, int userId, InventoryCardFromBizToAccTransferSaveDto saveRequest)
        {
            return PostAsync<InventoryCardFromBizToAccTransferSaveDto, InventoryCardFromBizToAccTransferResponseDto>(
                $"/InventoryCardApi/CreateInventoryCardFromBizToAccTransfer?firmId={firmId}&userId={userId}",
                saveRequest);
        }

        public Task<Dictionary<long, InventoryCardDto>> GetByPrimaryDocumentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new Dictionary<long, InventoryCardDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, Dictionary<long, InventoryCardDto>>(
                $"/InventoryCardApi/GetByPrimaryDocumentBaseIds?firmId={firmId}&userId={userId}",
                baseIds);
        }
    }
}
