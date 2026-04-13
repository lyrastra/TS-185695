using Moedelo.Authorities.ApiClient.Abstractions;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.NetFramework.Sfr
{
    [InjectAsSingleton(typeof(ISfrDepartmentsApiClient))]
    public class SfrDepartmentsApiClient : BaseCoreApiClient, ISfrDepartmentsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SfrDepartmentsApiClient(
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
            apiEndPoint = settingRepository.Get("SfrApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<IReadOnlyCollection<SfrDepartmentDto>> GetByRegionCodeAsync(string regionCode)
        {
            var tokenHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<SfrDepartmentDto[]>>($"/Regions/{regionCode}/Departments", queryHeaders: tokenHeaders);
            return response.data;
        }

        public async Task<SfrDepartmentDto> GetByCodeAsync(string code)
        {
            var tokenHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            var response = await GetAsync<ApiDataResult<SfrDepartmentDto>>($"/Departments/{code}", queryHeaders: tokenHeaders);
            return response.data;
        }

        public async Task<SfrDepartmentDto> GetByIdAsync(int id)
        {
            var tokenHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<SfrDepartmentDto>>($"/Departments{id}", queryHeaders: tokenHeaders);
            return response.data;
        }
    }
}
