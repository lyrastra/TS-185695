using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Country;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.Country
{
    [InjectAsSingleton]
    public class CountryApiClient : BaseApiClient, ICountryApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public CountryApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Country/V2";
        }

        public Task<List<CountryDto>> GetAllAsync()
        {
            return GetAsync<List<CountryDto>>("/GetAll", null);
        }

        public Task<string> GetAlpha3ByIsoAsync(string iso)
        {
            return GetAsync<string>("/GetAlpha3ByIso", new {iso});
        }

        public Task<string> GetIsoByAlpha3Async(string alpha3)
        {
            return GetAsync<string>("/GetIsoByAlpha3", new {alpha3});
        }

        public Task<string> GetNameByAlpha3Async(string alpha3)
        {
            return GetAsync<string>("/GetNameByAlpha3", new {alpha3});
        }
    }
}