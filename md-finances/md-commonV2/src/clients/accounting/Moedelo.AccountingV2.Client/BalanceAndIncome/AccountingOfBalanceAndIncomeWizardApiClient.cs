using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.BalanceAndIncome
{
    [InjectAsSingleton(typeof(IAccountingOfBalanceAndIncomeWizardApiClient))]
    public class AccountingOfBalanceAndIncomeWizardApiClient : BaseApiClient, IAccountingOfBalanceAndIncomeWizardApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AccountingOfBalanceAndIncomeWizardApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task CreatePostings(int firmId, int userId, long wizardStateId, bool ignoreWizardState = false)
        {
            return PostAsync($"/AccountingOfBalanceAndIncomeWizard/CreatePostings?firmId={firmId}&userId={userId}&wizardStateId={wizardStateId}&ignoreWizardState={ignoreWizardState}",
                 setting: new HttpQuerySetting { Timeout = TimeSpan.FromMinutes(2) });
        }
    }
}
