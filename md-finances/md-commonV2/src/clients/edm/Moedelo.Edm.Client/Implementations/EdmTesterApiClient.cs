using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Kontragents;
using Moedelo.Edm.Client.Contracts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class EdmTesterApiClient : BaseApiClient, IEdmTesterApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmTesterApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
            httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}/Rest/Tester";
        }

        public Task SetUpInvitationAsync(int firmId, int kontragentId, string provider, KontragentEdmInteractionStatus status)
            => PostAsync("/SetUpInvitation", new {firmId, kontragentId, provider, status});

        public Task SendInviteAsync(int firmId, int userId, int kontragentId, string provider)
            => PostAsync("/SendInvite", new {firmId, userId, kontragentId, provider});
    }
}
