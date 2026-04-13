using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.BillingApi
{
    public interface IBillingApiClient : IDI
    {
        [Obsolete("Метод готовится к удалению")]
        Task<List<RepeatPaymentResponseDto>> RepeatPaymentsAsync(List<RepeatPaymentRequestDto> paymentsDtos);

        Task<List<PaymentHistoryWithExDto>> GetPaymentHistoryWithExByIdsAsync(
            IReadOnlyCollection<int> ids);

        Task<List<PaymentHistoryWithExDto>> GetPaymentHistoryWithExByFirmIdsAsync(IEnumerable<int> firmIds);

        Task<bool?> IsLastPaymentAsync(int paymentId, bool? success);

        Task<Dictionary<int, int[]>> GetPaymentGroupsAsync(
            IReadOnlyCollection<int> paymentIds);
        /// <summary>
        /// действующий у фирмы платёж:
        /// - startDate меньше текущего момента,
        /// - expirationDate больше текущего момента
        /// если нужен платёж, на основании которого рассчитаны права, лучше вызвать GetLastTariffWithTrialAsync 
        /// </summary>
        Task<PaymentHistoryDto> GetCurrentPaymentWithTrialAsync(int firmId);
        /// <summary>
        /// действующий или последний просроченный платёж:
        /// - startDate меньше текущего момента,
        /// - expirationDate может быть как больше текущего момента, так и меньше (если нет иных платежей)
        /// </summary>
        Task<PaymentHistoryDto> GetLastTariffWithTrialAsync(int firmId);
        Task<TariffDto> GetCurrentTariffAsync(int firmId);

        /// <summary>
        /// update PaymentHistory, если есть id, insert если нет
        /// </summary>
        /// <param name="paymentHistory"></param>
        /// <returns>возращает id добавленного PaymentHistory</returns>
        Task<int> SavePaymentHistoryAsync(PaymentHistoryDto paymentHistory);
        
        Task<int> SavePaymentHistoryAndUpdatePositionsAsync(PaymentHistoryAndPositionsDto paymentHistoryAndPositions);

        Task<ShortTariffInfoDto> GetShortCurrentTariffInfoAsync(int firmId);

        Task<List<PositionByPaymentDto>> GetActsByPaymentAsync(int paymentId);

        Task SwitchIsRefundStateAsync(SwitchIsRefundStateRequestDto dto);

        Task<List<PaymentHistoryForBillingDto>> GetPaymentHistoryForBillingByFirmIdAsync(int firmId);

        [Obsolete("use IPaymentHistoryApiClient.UpdatePositionsAsync")]
        Task UpdatePaymentPositionsAsync(UpdatePaymentPositionsDto dto);

        [Obsolete("use IPaymentHistoryApiClient.GetPositionsAsync. Готовится к удалению (можно удалять)")]
        Task<List<PaymentPositionDto>> GetPaymentHistoryPositions(int paymentId);

        /// <summary> Получить дополнительные данные, используемые при отправке счета </summary>
        /// <returns>Дополнительные данные, используемые при отправке счета</returns>
        Task<PaymentHistoryExBillDataDto> GetPaymentHistoryExBillDataAsync(int paymentId);

        /// <summary> Получить дополнительные данные, используемые при отправке счета </summary>
        /// <returns>Дополнительные данные, используемые при отправке счета</returns>
        Task<List<PaymentHistoryExBillDataDto>> GetPaymentHistoryExBillDataAsync(IReadOnlyCollection<int> paymentIds);

        /// <summary> Сохранить дополнительные данные, используемые при отправке счета </summary>
        /// <param name="dto">Дополнительные данные, используемые при отправке счета</param>
        [Obsolete("эти данные не надо сохранять отдельно. Это внутреннее дело домена. Метод скоро будет удалён")]
        Task SavePaymentHistoryExBillDataAsync(PaymentHistoryExBillDataDto dto);

        /// <summary> Подтвердить оплаты у платежей (dbo.PaymentHistory) </summary>
        Task SwitchOnPaymentsAsync(List<int> paymentIds);

        Task<List<BillingDto>> GetBillingDataAsync(int firmId);

        Task<List<GroupedPaymentHistoryDto>> GetRealGroupedPaymentHistoryDtosForFirm(int firmId);

        Task<List<PaymentHistoryWithExDto>> GetSuccessPaymentsForPeriodByPaymentMethodsAsync(GetPaymentsForPeriodByPaymentMethodsRequestDto dto);

        Task<int> AddTrialPaymentAsync(AddTrialPaymentRequestDto dto);

        Task<int> SavePaymentHistoryExAsync(PaymentHistoryWithExDto dto);

        Task<bool> IsSubscriptionExpiredAsync(int firmId);

        Task<UserPaymentExtendedDto> GetCurrentUserPaymentAsync(int firmId);

        Task<UserPaymentExtendedDto[]> GetExtendedInfoAboutSuccessPaymentsAsync(int firmId);

        Task<PaymentHistoryDto> GetPaymentByIdAsync(int id);

        Task<List<PaymentHistoryDto>> GetPaymentHistoryForFirmAsync(int firmId);

        Task<List<int>> GetTrialOrPaidFirmsAsync(IReadOnlyCollection<int> firmIds);

        Task<SberbankPaymentsDto> GetSberbankPaymentsAsync(SberbankPaymentStatus status,
            string paymentMethod,
            int afterPaymentId,
            bool excludePaymentsWithEmptySberbankId,
            int? limit);

        [Obsolete("Используй GetSberbankPaymentsAsync")]
        Task<SberbankPaymentsDto> GetAllSberbankPaymentsByStatusAsync(SberbankPaymentStatus status,
            string paymentMethod);

        Task<PaymentHistoryDto> PayAsync(PayRequestDto payDto);

        Task<TariffDto> GetTariffByPriceListIdAsync(int id);

        Task<PaymentHistoryDto> GetPaymentByTransactionId(int id);

        Task DeletePaymentAsync(int id);

        Task SwitchOnPaymentAsync(SwitchOnPaymentRequestDto request);

        Task SwitchOffPaymentAsync(SwitchOffPaymentRequestDto requestDto);

        Task<IList<TariffNameDto>> GetTariffNamesByPriceListIdsAsync(IReadOnlyList<int> ids);

        Task<DataRequestWrapper<bool>> ProlongCurrentPaymentAsync(int firmId, int userId, int companyId);

        Task UpdateSberbankPaymentStatuses(List<UpdateSberbankPaymentStatusDto> dtos);

        [Obsolete("Use ICreateBillClient.CreateAsync")]
        Task<PaymentHistoryDto> PayWithBillAsync(PayRequestDto payDto);

        Task<PaymentHistoryDto> GetFirstUnsuccessfulPaymentForGroupAsync(int anyPaymentIdInGroup, long transactionId);

        Task ExpirePaymentForFirmsAsync(IReadOnlyCollection<int> firmIds);

        Task LinkPayPartPayments(IReadOnlyCollection<int> paymentIds);

        Task MarkPaymentsAsTrackedAsync(int firmId, IReadOnlyCollection<int> paymentIds);
    }
}
