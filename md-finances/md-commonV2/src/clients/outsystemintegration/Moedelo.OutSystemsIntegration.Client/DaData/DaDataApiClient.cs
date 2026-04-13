using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OutSystemsIntegrationV2.Dto.DaData.Banks;
using System;

namespace Moedelo.OutSystemsIntegrationV2.Client.DaData
{
    [InjectAsSingleton]
    public class DaDataApiClient : BaseApiClient, IDaDataApiClient
    {
        private readonly SettingValue apiEndpoint;

        public DaDataApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager, 
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("OutSystemsIntegrationApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<BankResponseDto> GetBankByBik(string bik)
        {
            if (string.IsNullOrEmpty(bik))
            {
                throw new ArgumentException("bik cannot be null or empty");
            }
            return GetAsync<BankResponseDto>("/DaData/BankApi/GetByBik", new { bik });
        }
    }
}