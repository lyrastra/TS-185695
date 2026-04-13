using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Base.Client.EdmVersion
{
    [InjectAsSingleton(typeof(IEdmVersionApiClient))]
    public class EdmVersionApiClient : BaseCoreApiClient, IEdmVersionApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmVersionApiClient(
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
            apiEndpoint = settingRepository.Get("EdmBaseApiEndpoint");
        }

        protected override string GetApiEndpoint() => apiEndpoint.Value;

        public async Task<int> GetEdmVersionAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            var headers = await GetPrivateTokenHeaders(firmId, userId, cancellationToken)
                .ConfigureAwait(false);

            return await GetAsync<int>("/api/v1/version", queryHeaders: headers, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        } 
    }
}