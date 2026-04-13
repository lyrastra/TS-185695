using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpsV2.Dto.HeaderData;

namespace Moedelo.SpsV2.Client.HeaderData
{
    [InjectAsSingleton(typeof(IHeaderDataApiClient))]
    public class HeaderDataApiClient : BaseApiClient, IHeaderDataApiClient
    {
        private readonly SettingValue apiEndpoint;

        private const string controllerUri = "/Rest/HeaderData";

        private const string getHeaderDataUri = "/GetHeaderData";

        public HeaderDataApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<HeaderDataDto> GetHeaderDataAsync()
        {
            return GetAsync<HeaderDataDto>(getHeaderDataUri);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{controllerUri}";
        }
    }
}