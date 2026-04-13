using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Wizard;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Wizard
{
    [InjectAsSingleton(typeof(IWizardApiClient))]
    public class WizardApiClient : BaseApiClient, IWizardApiClient
    {
        private readonly SettingValue apiEndPoint;

        public WizardApiClient(IHttpRequestExecutor httpRequestExecutor,
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

        public Task<long> OpenAsync(int firmId, int userId, WizardType type, int period, int year)
        {
            return PostAsync<long>($"/Wizard/Open?firmId={firmId}&userId={userId}&type={type}&period={period}&year={year}");
        }
    }
}