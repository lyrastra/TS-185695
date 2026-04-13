using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OutSystemsIntegrationV2.Dto.ExchangeRates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.OutSystemsIntegrationV2.Client.ExchangeRates
{
    [InjectAsSingleton]
    public class ExchangeRatesClient : BaseApiClient, IExchangeRatesClient
    {
        private readonly SettingValue apiEndpoint;
        
        public ExchangeRatesClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ExchangeRatesApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/V2";
        }

        public Task<List<GetExchangeRatesResponseDto>> GetExchangeRatesAsync(GetExchangeRatesRequestDto request)
        {
            return PostAsync<GetExchangeRatesRequestDto, List<GetExchangeRatesResponseDto>>("/GetExchangeRates", request);
        }
    }
}