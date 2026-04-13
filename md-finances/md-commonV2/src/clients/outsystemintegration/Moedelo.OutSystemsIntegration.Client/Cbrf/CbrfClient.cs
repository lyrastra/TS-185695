using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OutSystemsIntegrationV2.Dto.ExchangeRates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.OutSystemsIntegrationV2.Client.Cbrf
{
    [InjectAsSingleton]
    public class CbrfClient : BaseApiClient, ICbrfClient
    {
        private readonly SettingValue apiEndpoint;
        
        public CbrfClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("CbrfApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest";
        }

        public Task<List<GetExchangeRatesResponseDto>> GetExchangeRatesAsync(GetExchangeRatesRequestDto request)
        {
            return PostAsync<GetExchangeRatesRequestDto, List<GetExchangeRatesResponseDto>>("/ExchangeRates/V1/GetExchangeRates", request);
        }
    }
}