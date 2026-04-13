using Moedelo.AccountingV2.Dto.AccountingStatement;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.AccountingStatement
{
    [InjectAsSingleton(typeof(ISelfCostAccountingStatementsClient))]
    public class SelfCostAccountingStatementsClient : BaseCoreApiClient, ISelfCostAccountingStatementsClient
    {
        private readonly SettingValue endPointSetting;

        public SelfCostAccountingStatementsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            endPointSetting = settingRepository.Get("AccountingStatementsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endPointSetting.Value;
        }

        public async Task<IReadOnlyCollection<SelfCostTaxGetResponseDto>> GetAsync(int firmId, int userId, SelfCostTaxGetByPeriodDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return (await PostAsync<SelfCostTaxGetByPeriodDto, ApiDataResult<IReadOnlyCollection<SelfCostTaxGetResponseDto>>>($"/private/api/v1/SelfCostTax/GetByPeriod", request, queryHeaders: tokenHeaders)
                .ConfigureAwait(false))
                .data;
        }
    }
}
