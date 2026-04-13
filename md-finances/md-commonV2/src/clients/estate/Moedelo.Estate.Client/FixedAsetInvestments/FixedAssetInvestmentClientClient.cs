using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Estate.Client.FixedAsetInvestments.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Estate.Client.FixedAsetInvestments
{
    [InjectAsSingleton]
    public class FixedAssetInvestmentClientClient : BaseApiClient, IFixedAssetInvestmentClientClient
    {
        private readonly SettingValue apiEndpoint;

        public FixedAssetInvestmentClientClient(
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
        
        public Task<FixedAssetInvestmentDto> GetByBaseIdAsync(int firmId, int userId, long fixedAssetBaseId)
        {
            return GetAsync<FixedAssetInvestmentDto>(
                "/FixedAssetInvestmentApi/Get", 
                new { firmId, userId, baseId = fixedAssetBaseId });
        }

        public Task<List<FixedAssetInvestmentDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<FixedAssetInvestmentDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<FixedAssetInvestmentDto>>(
                $"/FixedAssetInvestmentApi/GetByIds?firmId={firmId}&userId={userId}",
               ids);
        }

        public Task UpdateFromBalancesAsync(int firmId, int userId, IReadOnlyCollection<FixedAssetInvestmentBalanceSaveDto> balances)
        {
            if (balances == null)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/FixedAssetInvestmentApi/SaveFromBalances?firmId={firmId}&userId={userId}", balances);
        }
    }
}