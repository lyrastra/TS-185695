using Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank;
using Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest;
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

namespace Moedelo.BankIntegrationsV2.Client.SberbankPaymentRequest
{
    [InjectAsSingleton]
    public class SberbankPaymentRequestClient : BaseApiClient, ISberbankPaymentRequestClient
    {
        private const string ControllerName = "/SberbankPaymentRequest/";
        private readonly SettingValue apiEndPoint;

        public SberbankPaymentRequestClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<GetSberbankPaymentStatusResponseDto> GetPaymentRequestStatusAsync(GetPaymentRequestStatusRequestDto dto)
        {
            return PostAsync<GetPaymentRequestStatusRequestDto, GetSberbankPaymentStatusResponseDto>("GetPaymentRequestStatus", dto);
        }

        public Task<AdvanceAcceptanceReponseDto> GetAllAdvanceAcceptancesAsync()
        {
            return GetAsync<AdvanceAcceptanceReponseDto>("GetAllAdvanceAcceptances");
        }

        [Obsolete("Надо использовать GetAllowedAdvanceAcceptancesByFirmIdListAsync")]
        public Task<AdvanceAcceptanceReponseDto> GetAllowedAdvanceAcceptancesByFirmAsync(int firmId)
        {
            return GetAsync<AdvanceAcceptanceReponseDto>("GetAllowedAdvanceAcceptancesByFirm", new { firmId });
        }

        public Task<AdvanceAcceptanceReponseDto> GetAllowedAdvanceAcceptancesByFirmIdListAsync(
            IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<GetAllowedAdvanceAcceptancesByFirmIdListDto, AdvanceAcceptanceReponseDto>(
                "GetAllowedAdvanceAcceptancesByFirmIdListAsync", 
                new GetAllowedAdvanceAcceptancesByFirmIdListDto {
                    FirmIdList = firmIds
                });
        }

        public Task<AdvanceAcceptanceReponseDto> GetRawAcceptancesByInnAsync(string inn)
        {
            return GetAsync<AdvanceAcceptanceReponseDto>("GetRawAcceptancesByInn", new { inn });
        }

        public Task<UpdateCreatedPaymentsStatusesResponseDto> UpdateCreatedPaymentsStatusesAsync(int limit, int minPayId, HttpQuerySetting httpQuerySetting = null)
        {
            return GetAsync<UpdateCreatedPaymentsStatusesResponseDto>("UpdateCreatedPaymentsStatuses", new { limit, minPayId }, setting: httpQuerySetting);
        }

        public Task SyncAdvanceAcceptancesAsync(SyncAdvanceAcceptancesRequestDto dto, HttpQuerySetting httpQuerySetting = null)
        {
            return PostAsync("SyncAdvanceAcceptances", dto, setting: httpQuerySetting);
        }

        public Task<SendManualPaymentRequestResponseDto> SendManualPaymentRequest(SendManualPaymentRequestRequestDto dto, HttpQuerySetting httpQuerySetting = null)
        {
            return PostAsync<SendManualPaymentRequestRequestDto, SendManualPaymentRequestResponseDto>("SendManualPaymentRequest", dto, setting: httpQuerySetting);
        }

        public Task<GetTariffAndAcceptancePairInfoDto> GetTariffAndAcceptancePairInfo(int firmId, int tariffId)
        {
            return GetAsync<GetTariffAndAcceptancePairInfoDto>("GetTariffAndAcceptancePairInfo", new { firmId, tariffId });
        }

        public Task<VerifiedClientsByAcceptanceResponseDto> VerifiedClientsByAcceptanceAsync(
            int verifiedLimit = 0, 
            int minAcceptanceId = 0,
            HttpQuerySetting httpQuerySetting = null)
        {
            return GetAsync<VerifiedClientsByAcceptanceResponseDto>("VerifiedClientsByAcceptance", new {verifiedLimit, minAcceptanceId },
                setting: httpQuerySetting);
        }

        public Task<List<SberbankIntegrationDataDto>> GetSberbankIntegrationDataByFirmIdListAsync(List<int> firmIds)
        {
            return PostAsync<GetSberbankIntegrationDataByFirmIdListDto, List<SberbankIntegrationDataDto>>(
                "GetSberbankIntegrationDataByFirmIdListAsync", 
                new GetSberbankIntegrationDataByFirmIdListDto  { FirmIds = firmIds});
        }

        public Task LinkAdvanceAcceptancesToFirmsAsync()
        {
            return GetAsync("LinkUnlinkedAcceptances");
        }
        
        public Task<CategorizePaymentsResponseDto> GetCategorizePaymentsAsync(List<int> firmIds)
        {
            return PostAsync<CategorizePaymentsRequestDto, CategorizePaymentsResponseDto>(
                "CategorizePayments", 
                new CategorizePaymentsRequestDto { FirmIds = firmIds});
        }
    }
}