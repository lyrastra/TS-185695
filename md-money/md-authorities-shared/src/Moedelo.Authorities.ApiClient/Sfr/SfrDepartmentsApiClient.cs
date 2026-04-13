using Microsoft.Extensions.Logging;
using Moedelo.Authorities.ApiClient.Abstractions;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.Sfr
{
    [InjectAsSingleton(typeof(ISfrDepartmentsApiClient))]
    internal class SfrDepartmentsApiClient : BaseApiClient, ISfrDepartmentsApiClient
    {
        public SfrDepartmentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SfrFirmRequisitesApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("SfrApiEndpoint"),
                  logger)
        {
        }

        public async Task<IReadOnlyCollection<SfrDepartmentDto>> GetByRegionCodeAsync(string regionCode)
        {
            var response = await GetAsync<ApiDataResult<SfrDepartmentDto[]>>($"/Regions/{regionCode}/Departments");
            return response.data;
        }

        public async Task<SfrDepartmentDto> GetByCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            var response = await GetAsync<ApiDataResult<SfrDepartmentDto>>($"/Departments/{code}");
            return response.data;
        }

        public async Task<SfrDepartmentDto> GetByIdAsync(int id)
        {
            var response = await GetAsync<ApiDataResult<SfrDepartmentDto>>($"/Departments/{id}");
            return response.data;
        }
    }
}
