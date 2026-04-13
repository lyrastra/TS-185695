using System;
using Microsoft.Extensions.Logging;
using Moedelo.CommissionAgents.ApiClient.Abstractions;
using Moedelo.CommissionAgents.ApiClient.Abstractions.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.KidirOsno.ApiClient.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.CommissionAgents.ApiClient
{
    [InjectAsSingleton(typeof(ICommissionAgentsApiClient))]
    class CommissionAgentsApiClient : BaseApiClient, ICommissionAgentsApiClient
    {
        public CommissionAgentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CommissionAgentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("CommissionAgentsApiEndpoint"),
                logger)
        {
        }

        public Task<CommissionAgentDto> GetByKontragentIdAsync(int kontragentId)
        {
            return GetAsync(kontragentId, null);
        }

        public Task<CommissionAgentDto> GetByInnAsync(string inn)
        {
            return GetAsync(null, inn);
        }

        public async Task<IReadOnlyCollection<CommissionAgentDto>> GetAutocompleteAsync(string query, int count)
        {
            
            var url = $"/api/v1/CommissionAgent/Autocomplete?query={System.Net.WebUtility.UrlEncode(query)}&count={count}";
            var response = await GetAsync< ApiDataDto<IReadOnlyCollection<CommissionAgentDto>>>(url);
            return response.data;
        }

        public async Task<AccessDto> HasAccessAsync()
        {
            var response = await GetAsync<ApiDataDto<AccessDto>>($"/api/v1/Data/Setup");
            return response.data;
        }

        public async Task<CommissionAgentDto> GetByIdAsync(int id)
        {
            var url = $"/api/v1/CommissionAgent/{id}";
            var response = await GetAsync<ApiDataDto<CommissionAgentDto>>(url);
            return response.data;
        }

        public async Task<IReadOnlyList<CommissionAgentDto>> GetByIdsAsync(IReadOnlyList<int> ids)
        {
            if (ids?.Any() != true)
            {
                return Array.Empty<CommissionAgentDto>();
            }
            
            var url = "/api/v1/CommissionAgent/byIds";
            var response = await PostAsync<IReadOnlyList<int>, ApiDataDto<IReadOnlyList<CommissionAgentDto>>>(url, ids);
            return response.data;
        }

        private async Task<CommissionAgentDto> GetAsync(int? kontragentId, string inn)
        {
            var url = $"/api/v1/CommissionAgent";
            var response = await GetAsync<ApiDataDto<CommissionAgentDto[]>>(url, new { kontragentId, inn });
            return response.data.SingleOrDefault();
        }
    }
}
