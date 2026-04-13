using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccPostings.Client
{
    [InjectAsSingleton]
    public class SyntheticAccountClient : BaseApiClient, ISyntheticAccountClient
    {
        private readonly SettingValue apiEndPoint;

        public SyntheticAccountClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
            : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccPostingsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
        
        public Task<List<SyntheticAccountDto>> GetActualAsync()
        {
            return GetAsync<List<SyntheticAccountDto>>("/SyntheticAccount/Actual");
        }

        public Task<List<SyntheticAccountTypeDto>> GetByIdsAsync(IReadOnlyCollection<long> accountTypeIds)
        {
            if (accountTypeIds?.Any() != true)
            {
                return Task.FromResult(new List<SyntheticAccountTypeDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<SyntheticAccountTypeDto>>(
                "/SyntheticAccount/ByIds",
                accountTypeIds);
        }

        public Task<List<SyntheticAccountTypeDto>> GetByCodesAsync(IReadOnlyCollection<SyntheticAccountCode> codes)
        {
            if (codes?.Any() != true)
            {
                return Task.FromResult(new List<SyntheticAccountTypeDto>());
            }

            return PostAsync<IReadOnlyCollection<SyntheticAccountCode>, List<SyntheticAccountTypeDto>>(
                "/SyntheticAccount/ByCodes",
                codes);
        }

        public Task<List<SyntheticAccountSubcontoRuleDto>> GetRulesByAccountIdsAsync(IReadOnlyCollection<SyntheticAccountCode> codes)
        {
            if (codes?.Any() != true)
            {
                return Task.FromResult(new List<SyntheticAccountSubcontoRuleDto>());
            }

            return PostAsync<IReadOnlyCollection<SyntheticAccountCode>, List<SyntheticAccountSubcontoRuleDto>>(
                "/SyntheticAccount/RulesByCodes",
                codes);
        }
    }
}
