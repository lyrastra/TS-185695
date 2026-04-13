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
    [InjectAsSingleton(typeof(ISfrAutocompleteApiClient))]
    public class SfrAutocompleteApiClient : BaseApiClient, ISfrAutocompleteApiClient
    {
        public SfrAutocompleteApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SfrAutocompleteApiClient> logger)
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

        public async Task<IReadOnlyCollection<SfrDepartmentWithRequisitesAutocompleteItemDto>> GetDepartmentWithRequisitesByCodeAsync(string query, int count = 5)
        {
            var response = await GetAsync<ApiDataResult<SfrDepartmentWithRequisitesAutocompleteItemDto[]>>(
                $"/Autocompletes/DepartmentWithRequisitesAutocomplete",
                new { query, count });
            return response.data;
        }
    }
}
