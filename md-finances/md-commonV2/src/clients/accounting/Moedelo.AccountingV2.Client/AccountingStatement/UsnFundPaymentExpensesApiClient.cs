using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AccountingStatement;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.AccountingStatement
{
    [InjectAsSingleton(typeof(IUsnFundPaymentExpensesApiClient))]
    internal class UsnFundPaymentExpensesApiClient : BaseCoreApiClient, IUsnFundPaymentExpensesApiClient
    {
        private readonly SettingValue endPointSetting;

        public UsnFundPaymentExpensesApiClient(
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

        public async Task<UsnFundPaymentExpensesStatementStatus> GetStatusAsync(int firmId, int userId, int year)
        {
            var uri = $"/api/v1/UsnFundPaymentExpenses/status/{year}";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            return (await GetAsync<ApiDataResult<UsnFundPaymentExpensesStatementStatus>>(uri, queryHeaders: tokenHeaders).ConfigureAwait(false))
                .data;
        }
    }
}
