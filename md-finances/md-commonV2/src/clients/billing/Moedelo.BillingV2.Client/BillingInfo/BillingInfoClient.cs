using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.BillingInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.BillingInfo
{
    [InjectAsSingleton]
    public class BillingInfoClient : BaseApiClient, IBillingInfoClient
    {
        private readonly SettingValue apiEndPoint;
        
        public BillingInfoClient(
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
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        public Task<List<LastPaymentsForFirmsInPeriodResponseDto>> GetLastPaymentsForFirmsInPeriodAsync(
            LastPaymentsForFirmsInPeriodRequestDto dto)
        {
            return PostAsync<LastPaymentsForFirmsInPeriodRequestDto, List<LastPaymentsForFirmsInPeriodResponseDto>>
                ("/BillingInfo/GetLastPaymentsForFirmsInPeriod", dto);
        }

        public Task<List<FirmsTransitionTypesByPaymentsResponseDto>> GetFirmsTransitionTypesByPaymentsAsync(
            FirmsTransitionTypesByPaymentsRequestDto dto)
        {
            return PostAsync<FirmsTransitionTypesByPaymentsRequestDto, List<FirmsTransitionTypesByPaymentsResponseDto>>
                ("/BillingInfo/GetFirmsTransitionTypesByPayments", dto);
        }

        public Task<List<RenewSubscriptionsCountSummaryForFirmsResponseDto>> GetRenewSubscriptionsCountSummaryAsync(
            RenewSubscriptionsCountSummaryForFirmsRequestDto dto)
        {
            return PostAsync<RenewSubscriptionsCountSummaryForFirmsRequestDto,
                    List<RenewSubscriptionsCountSummaryForFirmsResponseDto>>
                ("/BillingInfo/GetRenewSubscriptionsCountSummary", dto);
        }

        public Task<List<RenewSubscriptionsInfoForFirmResponseDto>> GetRenewSubscriptionsInfoForFirmsAsync(
            RenewSubscriptionsInfoForFirmsRequestDto dto)
        {
            return PostAsync<RenewSubscriptionsInfoForFirmsRequestDto, List<RenewSubscriptionsInfoForFirmResponseDto>>
                ("/BillingInfo/GetRenewSubscriptionsInfoForFirms", dto);
        }

        public Task<List<RealPaymentCountsForFirmsResponseDto>> GetRealPaymentCountsForFirmsAsync(
            RealPaymentCountsForFirmsRequestDto dto)
        {
            return PostAsync<RealPaymentCountsForFirmsRequestDto,List<RealPaymentCountsForFirmsResponseDto>>
                ("/BillingInfo/GetRealPaymentCountsForFirms", dto);
        }
        
        public Task<List<PaymentsWithExForFirmResponseDto>> GetPaymentsWithExForFirmsAsync(
            PaymentsWithExForFirmsRequestDto dto)
        {
            return PostAsync<PaymentsWithExForFirmsRequestDto, List<PaymentsWithExForFirmResponseDto>>
                ("/BillingInfo/GetPaymentsWithExForFirms", dto);
        }

        public Task<List<FirmsPayedTypesByPaymentsResponseDto>> GetFirmsPayedTypesByPaymentsAsync(FirmsPayedTypesByPaymentsRequestDto dto)
        {
            return PostAsync<FirmsPayedTypesByPaymentsRequestDto, List<FirmsPayedTypesByPaymentsResponseDto>>
                ("/BillingInfo/GetFirmsPayedTypesByPayments", dto);
        }

        public Task<List<GeneralRenewSubscriptionsReportRowDto>> GetGeneralRenewSubscriptionsReportRowsAsync(
            GeneralRenewSubscriptionsReportRowsRequestDto dto, HttpQuerySetting querySetting = null)
        {
            return PostAsync<GeneralRenewSubscriptionsReportRowsRequestDto, List<GeneralRenewSubscriptionsReportRowDto>>(
                "/BillingInfo/GeneralRenewSubscriptionsReport/GetBillingRows",
                dto, setting: querySetting);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}