using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.CourierRegion;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.CourierRegion
{
    [InjectAsSingleton]
    public class CourierRegionClient : BaseApiClient, ICourierRegionClient
    {
        private readonly SettingValue apiEndPoint;

        public CourierRegionClient(IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<CourierRegionDto>> GetAllAsync()
        {
            var response = await GetAsync<List<CourierRegionDto>>("/CourierRegion/GetAll").ConfigureAwait(false);
            return response;
        }

        public async Task<List<string>> GetAllForAutocompleteAsync()
        {
            var response = await GetAsync<List<string>>("/CourierRegion/GetAllForAutocomplete").ConfigureAwait(false);
            return response;
        }

        public async Task<string> GetCourierRegionCodeByFirmCodeAsync(string code)
        {
            var response = await GetAsync<string>("/CourierRegion/GetCourierRegionCodeByFirmCode", new { code }).ConfigureAwait(false);
            return response;
        }

        public Task<int> SaveAsync(CourierRegionDto dto)
        {
            return PostAsync<CourierRegionDto, int>($"/CourierRegion/Save", dto);
        }

        public Task DeleteAsync(int id)
        {
            return PostAsync<object>($"/CourierRegion/Delete?id={id}");
        }

    }
}
