using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Tariff;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Tariff
{
    [InjectAsSingleton]
    public class TariffApiClient : BaseApiClient, ITariffApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public TariffApiClient(
           IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Tariff";
        }

        public Task<List<TariffDto>> GetAllAsync()
        {
            return GetAsync<List<TariffDto>>("/V2/GetAll", null);
        }

        public async Task<string> GetTariffDataAsync(int id)
        {
            var result = await GetAsync<DataWrapper<string>>($"/GetTariffData?id={id}", null).ConfigureAwait(false);
            return result.Data;
        }
    }
}