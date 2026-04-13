using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.DataInformation
{
    [InjectAsSingleton]
    public class BankIntegrationsDataInformationClient : BaseApiClient, IBankIntegrationsDataInformationClient
    {
        private const string ControllerName = "/DataInformation/";
        private readonly SettingValue apiEndPoint;

        public BankIntegrationsDataInformationClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<List<IntegrationRequestQueueStatusDto>> GetRequestQueueStatusAsync(int firmId)
        {
            return GetAsync<List<IntegrationRequestQueueStatusDto>>("GetRequestQueueStatus", new { firmId });
        }

        public Task<bool> HasUnprocessedRequestsAsync(int firmId, IntegrationPartners integrationPartner)
        {
            return GetAsync<bool>("HasUnprocessedRequests", new { firmId, integrationPartner });
        }

        public Task<DateTime?> GetSberbankSettlementsFirstCreateDateAsync(int firmId)
        {
            return GetAsync<DateTime?>("GetSberbankSettlementsFirstCreateDate", new { firmId });
        }

        public Task<DateTime?> GetMovementLastDateAsync(IntegrationPartners integrationPartner, int firmId)
        {
            return GetAsync<DateTime?>("GetMovementLastDate", new { integrationPartner, firmId });
        }

        public Task<IntSummaryBySettlementsResponseDto> GetIntSummaryBySettlementsAsync(IntSummaryBySettlementsRequestDto requestDto)
        {
            return PostAsync<IntSummaryBySettlementsRequestDto, IntSummaryBySettlementsResponseDto>("GetIntSummaryBySettlementsAsync", requestDto);
        }

        public Task<BackOfficeResponseDto> GetIntegrationRequestsToBackOfficeAsync(BackOfficeRequestDto dto)
        {
            return PostAsync<BackOfficeRequestDto, BackOfficeResponseDto>("GetIntegrationRequestsToBackOffice", dto);
        }

        public Task<BackOfficeLogResponseDto> GetIntegrationLogToBackOfficeAsync(int requestId)
        {
            return GetAsync<BackOfficeLogResponseDto>("GetIntegrationLogToBackOffice", new { requestId });
        }

        public Task<List<SettlementAccountV2Dto>> GetSberbankSettlementsAsync(int firmId)
        {
            return GetAsync<List<SettlementAccountV2Dto>>("GetSberbankSettlements", new { firmId });
        }
        
        public Task<List<SettlementAccountV2Dto>> GetSettlementAccountsByPartnerAsync(int firmId, int userId, IntegrationPartners partner)
        {
            return GetAsync<List<SettlementAccountV2Dto>>("GetSettlementAccountsByPartner", new { firmId, userId, partner });
        }

        public Task<GetSberbankPaymentStatusResponseDto> GetSberbankPaymentsStatusAsync(GetSberbankPaymentStatusRequestDto requestDto)
        {
            return PostAsync<GetSberbankPaymentStatusRequestDto, GetSberbankPaymentStatusResponseDto>("GetSberbankPaymentsStatus", requestDto);
        }
        
        public Task<bool> IsMoreThanOneSettlementAccountAsync(int firmId, int userId, IntegrationPartners partner)
        {
            return GetAsync<bool>($"IsMoreThanOneSettlementAccount?firmId={firmId}&userId={userId}&partner={partner}");
        }
    }
}