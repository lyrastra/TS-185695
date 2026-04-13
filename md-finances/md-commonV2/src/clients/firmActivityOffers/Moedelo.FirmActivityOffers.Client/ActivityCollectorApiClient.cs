using Moedelo.FirmActivityOffers.Client.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.FirmActivityOffers.Client
{
    [InjectAsSingleton(typeof(IActivityCollectorApiClient))]
    public class ActivityCollectorApiClient : BaseCoreApiClient, IActivityCollectorApiClient
    {
        private readonly SettingValue endpoint;

        public ActivityCollectorApiClient(
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
            endpoint = settingRepository.Get("FirmActivityOffersActivityCollectorApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task AddAsync(int firmId, int userId, AddActivityRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync("/api/v1/Activities", request, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}
