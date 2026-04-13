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
    [InjectAsSingleton(typeof(ISfrAutocompleteApiClient))]
    public class SfrAutocompleteApiClient : BaseCoreApiClient, ISfrAutocompleteApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SfrAutocompleteApiClient(
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

        public async Task<IReadOnlyCollection<SfrDepartmentWithRequisitesAutocompleteItemDto>> GetDepartmentWithRequisitesByCodeAsync(string query, int count = 5)
        {
            var tokenHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<SfrDepartmentWithRequisitesAutocompleteItemDto[]>>(
                $"/Autocompletes/DepartmentWithRequisitesAutocomplete",
                new { query, count }, queryHeaders: tokenHeaders);
            return response.data;
        }
    }
}
