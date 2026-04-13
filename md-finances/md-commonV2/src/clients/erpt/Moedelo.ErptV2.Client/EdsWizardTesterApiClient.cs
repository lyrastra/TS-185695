using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client
{
    public interface IEdsWizardTesterApiClient : IDI
    {
        Task ChangeWizardCaTargetToAstralAsync(int firmId);
        Task FinishWizardAsync(int firmId);
        Task ActivateTransferAstralToMoedeloAsync(int firmId);
    }

    [InjectAsSingleton]
    public class EdsWizardTesterApiClient : BaseApiClient, IEdsWizardTesterApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdsWizardTesterApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Tester/Eds";
        }

        public Task ChangeWizardCaTargetToAstralAsync(int firmId)
            => PostAsync($"/ChangeWizardCaTargetToAstral?firmId={firmId}");

        public Task FinishWizardAsync(int firmId)
            => PostAsync($"/FinishWizard?firmId={firmId}");

        public Task ActivateTransferAstralToMoedeloAsync(int firmId)
            => PostAsync($"/ActivateTransferAstralToMoedelo?firmId={firmId}");
    }
}
