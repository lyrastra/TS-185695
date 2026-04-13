using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpsV2.Dto.Populars;

namespace Moedelo.SpsV2.Client.Populars
{
    [InjectAsSingleton(typeof(IPopularApiClient))]
    public class PopularApiClient : BaseApiClient, IPopularApiClient
    {
        private readonly SettingValue apiEndpoint;

        private const string CONTROLLER_URI = "/Rest/Popular";

        private const string GET_POPULAR_LIST_URI = "/GetPopularList";

        public PopularApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<List<PopularDto>> GetPopularList(GetPopularListRequestDto request)
        {
            return GetAsync<List<PopularDto>>(GET_POPULAR_LIST_URI);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{CONTROLLER_URI}";
        }
    }
}