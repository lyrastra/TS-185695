using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationRequests;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.IntegrationRequests
{
    [InjectAsSingleton(typeof(IIntegrationRequestApiClient))]
    public class IntegrationRequestApiClient : BaseApiClient, IIntegrationRequestApiClient
    {
        private const string IntegratedUserController = "/private/api/v1/integration/request";

        public IntegrationRequestApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegrationRequestApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApiNetCore"),
                logger)
        {
        }

        public async Task<IntegrationRequestDto> GetLastIntegrationRequestByPartnerAsync(IntegrationPartners partner)
        {
            var url = $"{IntegratedUserController}/LastIntegrationRequestByPartner";
            var queryParams = new { partner };

            return await GetAsync<IntegrationRequestDto>(url, queryParams);
        }

        public Task<IntegrationRequestDto> GetByIdAsync(int requestId, CancellationToken cancellationToken)
        {
            var url = $"{IntegratedUserController}/{requestId}";

            return GetAsync<IntegrationRequestDto>(url, cancellationToken: cancellationToken);
        }

        public async Task<int> CreateNewAsync(NewIntegrationRequestDto requestDto)
        {
            var url = $"{IntegratedUserController}";

            return await PostAsync<NewIntegrationRequestDto, int>(url, requestDto);
        }

        public async Task SetStatusAsync(int requestId, RequestStatus status, string dateOfCall)
        {
            var url = $"{IntegratedUserController}/{requestId}/status";
            var body = new IntegrationRequestSetStatusRequestDto
            {
                Status = status,
                DateOfCall = dateOfCall
            };

            await PutAsync(url, body);
        }

        public async Task UpdateStatusAsync(int requestId, RequestStatus status, RequestStatus oldStatus)
        {
            var url = $"{IntegratedUserController}/{requestId}/status/change";
            var body = new IntegrationRequestUpdateStatusRequestDto
            {
                Status = status,
                OldStatus = oldStatus
            };

            await PutAsync(url, body);
        }

        public async Task AddHistoryAsync(IntegrationRequestNewHistoryDto dto)
        {
            var url = $"/private/api/v1/integration/request/{dto.RequestId}/history";

            await PutAsync(url, dto);
        }

        public Task UpdateIntegrationRequestsAsync(IntegrationRequestStatusesUpdateRequestDto request)
        {
            var url = $"{IntegratedUserController}/UpdateStatuses";

            return PutAsync(url, request);
        }

        public async Task<IReadOnlyList<FirmSettlementNumberDto>> GetSettlementNumbersWithoutRequestsInPeriodAsync(
            SettlementNumbersWithoutRequestsInPeriodRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            var url = $"{IntegratedUserController}/GetWithoutRequestsInPeriod";

            return await PostAsync<SettlementNumbersWithoutRequestsInPeriodRequestDto, FirmSettlementNumberDto[]>(
                    url,
                    requestDto,
                    cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<FirmSettlementNumberOnDateDto>> GetFirmSettlementNumbersWithoutRequestsInPeriodAsync(
            MissedMovementsRequestsFilterDto requestDto,
            CancellationToken cancellationToken)
        {
            var url = $"{IntegratedUserController}/GetFirmSettlementNumbersWithoutRequestsInPeriod";

            var numbers = await PostAsync<MissedMovementsRequestsFilterDto, FirmSettlementNumberOnDateDto[]>(
                    url,
                    requestDto,
                    cancellationToken: cancellationToken);

            return numbers;
        }

        public Task<int> CountRequestsByFilterAsync(
            CountIntegrationRequestsByFilterDto requestDto,
            CancellationToken cancellationToken)
        {
            var url = $"{IntegratedUserController}/CountByFilter";

            return PostAsync<CountIntegrationRequestsByFilterDto, int>(
                url,
                requestDto,
                cancellationToken: cancellationToken);
        }
    }
}
