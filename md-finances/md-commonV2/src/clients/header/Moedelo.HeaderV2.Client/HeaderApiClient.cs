using System.Threading.Tasks;
using Moedelo.HeaderV2.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HeaderV2.Client
{
    [InjectAsSingleton]
    public class HeaderApiClient : BaseApiClient, IHeaderApiClient
    {
        private readonly SettingValue apiEndPoint;

        public HeaderApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HeaderApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<HeaderDto> GetHeadAsync(HeaderRequestDto request)
        {
            return GetAsync<HeaderDto>("/Head", request);
        }

        public Task<HeaderDto> GetAccountControlHeadAsync(AccountControlHeaderRequestDto request)
        {
            return GetAsync<HeaderDto>("/AccountControlHead", request);
        }
    }
}
