using System;
using Moedelo.CommissionAgents.Client.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.CommissionAgents.Client
{
    [InjectAsSingleton]
    public class CommissionAgentsApiClient : BaseCoreApiClient, ICommissionAgentsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public CommissionAgentsApiClient(
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
            apiEndpoint = settingRepository.Get("CommissionAgentsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }


        public async Task<IReadOnlyList<CommissionAgentDto>> GetAsync(int firmId, int userId,
            CommissionAgentRequestDto request, CancellationToken cancellationToken)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, cancellationToken).ConfigureAwait(false);
            var result = await GetAsync<ApiDataResult<CommissionAgentDto[]>>("/api/v1/CommissionAgent",
                request,
                queryHeaders: tokenHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.data;
        }

        public async Task<bool> CanDeleteAsync(int firmId, int userId, int id)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await GetAsync<ApiDataResult<bool>>(
                $"/api/v1/CommissionAgent/CanDelete/{id:int}",
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return result.data;
        }

        public async Task<AccessDto> HasAccessAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<AccessDto>>(
                $"/api/v1/Data/Setup",
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<CommissionAgentCreateResultDto> CreateByInnAsync(int firmId, int userId, string inn)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<CommissionAgentCreateRequestDto, ApiDataResult<CommissionAgentCreateResultDto>>(
                $"/api/v1/CommissionAgent",
                new CommissionAgentCreateRequestDto {Inn = inn},
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<bool> IsExistsByInnAsync(int firmId, int userId, string inn)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<bool>>(
                $"/api/v1/CommissionAgent/IsExist/{inn}",
                new CommissionAgentCreateRequestDto {Inn = inn},
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<FromKontragent.CreateResultDto> CreateFromKontragentAsync(int firmId, int userId, FromKontragent.CreateRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<FromKontragent.CreateRequestDto, ApiDataResult<FromKontragent.CreateResultDto>>(
                $"/api/v1/CommissionAgent/CreateFromKontragent",
                request,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<FromKontragent.DeleteResultDto> DeleteFromKontragentAsync(int firmId, int userId, FromKontragent.DeleteRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<FromKontragent.DeleteRequestDto, ApiDataResult<FromKontragent.DeleteResultDto>>(
                $"/api/v1/CommissionAgent/DeleteFromKontragent",
                request,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
