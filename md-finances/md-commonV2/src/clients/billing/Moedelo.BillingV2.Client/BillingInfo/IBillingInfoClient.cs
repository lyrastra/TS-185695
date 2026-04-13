using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.BillingInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BillingV2.Client.BillingInfo
{
    /// <summary>Клиент для получения узкоспециализированных данных по биллингу</summary>
    public interface IBillingInfoClient : IDI
    {
        /// <summary>Получить последние платежи по дате окончания, находящиеся в промежутке дат</summary>
        Task<List<LastPaymentsForFirmsInPeriodResponseDto>> GetLastPaymentsForFirmsInPeriodAsync(
            LastPaymentsForFirmsInPeriodRequestDto dto);

        /// <summary>Получить типы перехода с тарифа на основе истории биллинга</summary>
        Task<List<FirmsTransitionTypesByPaymentsResponseDto>> GetFirmsTransitionTypesByPaymentsAsync(
            FirmsTransitionTypesByPaymentsRequestDto dto);
            
        /// <summary>Получить агрегированную информацию о переподписке</summary>
        Task<List<RenewSubscriptionsCountSummaryForFirmsResponseDto>> GetRenewSubscriptionsCountSummaryAsync(
            RenewSubscriptionsCountSummaryForFirmsRequestDto dto);

        /// <summary>Получить информацию о переподписке</summary>
        Task<List<RenewSubscriptionsInfoForFirmResponseDto>> GetRenewSubscriptionsInfoForFirmsAsync(
            RenewSubscriptionsInfoForFirmsRequestDto dto);

        /// <summary>Получить информацию о количестве платежей</summary>
        Task<List<RealPaymentCountsForFirmsResponseDto>> GetRealPaymentCountsForFirmsAsync(
            RealPaymentCountsForFirmsRequestDto dto);
            
        /// <summary>Получить списки платежей со счетами</summary>
        Task<List<PaymentsWithExForFirmResponseDto>> GetPaymentsWithExForFirmsAsync(
            PaymentsWithExForFirmsRequestDto dto);

        /// <summary>Получить статусы оплаченности фирм на основе истории биллинга</summary>
        Task<List<FirmsPayedTypesByPaymentsResponseDto>> GetFirmsPayedTypesByPaymentsAsync(
            FirmsPayedTypesByPaymentsRequestDto dto);

        Task<List<GeneralRenewSubscriptionsReportRowDto>> GetGeneralRenewSubscriptionsReportRowsAsync(
            GeneralRenewSubscriptionsReportRowsRequestDto dto, HttpQuerySetting querySetting = null);
    }
}