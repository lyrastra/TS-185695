using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.ApiClient.Dto;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequests;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationRequests
{
    [InjectAsSingleton(typeof(IIntegrationRequestApiClient))]
    internal sealed class IntegrationRequestApiClient : BaseCoreApiClient, IIntegrationRequestApiClient
    {
        private readonly SettingValue endpoint;
        
        public IntegrationRequestApiClient(
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
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task<IntegrationRequestDto> GetAsync(int firmId, IntegrationPartners partner, string requestId)
        {
            const string url = "/private/api/v1/integration/request/byRequestId";
            var queryParams = new { firmId, partner, requestId };
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<IntegrationRequestDto>(url, queryParams, queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<PaginatedCollectionDto<IntegrationRequestDto>> GetListAsync(
            IntegrationRequestsListFilterDto filterDto)
        {
            const string url = "/private/api/v1/integration/request/find";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<PaginatedCollectionDto<IntegrationRequestDto>>(url, filterDto, queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<IntegrationRequestDto> GetAsync(int id)
        {
            var url = $"/private/api/v1/integration/request/{id}";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<IntegrationRequestDto>(url, queryHeaders: queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<IntegrationRequestWithXmlHistoryDto[]> GetWithXmlHistoryAsync(IntegrationRequestsWithXmlHistoryFilterDto filterDto)
        {
            const string url = "/private/api/v1/integration/request/withXmlHistory";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<IntegrationRequestWithXmlHistoryDto[]>(url, filterDto, queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<bool> HasAsync(HasIntegrationRequestsCheckConditionsDto conditionsDto)
        {
            const string url = "/private/api/v1/integration/request/has";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<bool>(url, conditionsDto, queryHeaders: queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task SetStatusAsync(int requestId, RequestStatus status, string dateOfCall)
        {
            var url = $"/private/api/v1/integration/request/{requestId}/status";
            var body = new IntegrationRequestSetStatusRequestDto
            {
                Status = status,
                DateOfCall = dateOfCall
            };
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            await PutAsync(url, body, queryHeaders).ConfigureAwait(false);
        }

        public async Task AddHistoryAsync(IntegrationRequestNewHistoryDto dto)
        {
            var url = $"/private/api/v1/integration/request/{dto.RequestId}/history";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            await PutAsync(url, dto, queryHeaders).ConfigureAwait(false);
        }

        public async Task SetStatusAsync(SetIntegrationRequestsStatusDto requestDto)
        {
            const string url = "/private/api/v1/integration/request/status";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            await PutAsync(url, requestDto, queryHeaders).ConfigureAwait(false);
        }

        public async Task<int> CreateNewAsync(NewIntegrationRequestDto newRequestDto)
        {
            const string url = "/private/api/v1/integration/request";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await PostAsync<NewIntegrationRequestDto, int>(url, newRequestDto, queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<List<IntegrationPartnerRequestsInStatusCountDto>> CountPartnerRequestsAsync(
            PartnerIntegrationRequestsCountClaimDto dto)
        {
            const string url = "/private/api/v1/integration/request/count";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<List<IntegrationPartnerRequestsInStatusCountDto>>(url, dto, queryHeaders)
                .ConfigureAwait(false);
        }
        
        public async Task<List<IntegrationPartners>> HasUnprocessedRequestMovementListAsync(
            HasUnprocessedRequestMovementListDto dto)
        {
            const string url = "/private/api/v1/integration/request/hasUnprocessedRequestMovementList";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<List<IntegrationPartners>>(url, dto, queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<List<IntegrationPartnerRequestsInStatusCountDto>> CountPartnerRequestsAsync(
            PartnerIntegrationRequestDailyWithoutRepeatClaimDto dto)
        {
            const string url = "/private/api/v1/integration/request/countWithoutRepeat";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<List<IntegrationPartnerRequestsInStatusCountDto>>(url, dto, queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<List<IntegrationPartnerRequestsInStatusCountDto>> CountPartnerRequestsAsync(PartnerIntegrationRequestsCountFilterDto dto)
        {
            const string url = "/private/api/v1/integration/request/countByPartner";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<List<IntegrationPartnerRequestsInStatusCountDto>>(url, dto, queryHeaders)
                .ConfigureAwait(false);
        }

        public async Task<DateTime?> GetLastIntegrationRequestEndDateAsync(LastPartnerIntegrationRequestDateClaimDto dto)
        {
            const string url = "/private/api/v1/integration/request/lastDate";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            return await GetAsync<DateTime?>(url, dto, queryHeaders)
                .ConfigureAwait(false);
        }
        
        public async Task SendMissedMovementsRequestAsync(
            RequestMovementsMissingDaysRequestDto requestDto)
        {
            const string url = "/private/api/v1/integration/request/SendMissedMovementsRequest";
            var queryHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            await PostAsync(url, requestDto, queryHeaders, setting: new HttpQuerySetting(TimeSpan.FromMinutes(2)))
                .ConfigureAwait(false);
        }
    }
}
