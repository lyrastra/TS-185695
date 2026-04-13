using System.Collections.Generic;
using Moedelo.Billing.Abstractions.Dto.PartialRefund;
using System.Threading.Tasks;

namespace Moedelo.Billing.Abstractions.PartialRefund;

public interface IPaymentHistoryPartialRefundApiClient
{
    /// <summary>
    /// Возвращает данные по частичному возврату
    /// </summary>
    /// <param name="paymentHistoryId">Идентификатор платежа</param>
    Task<PaymentHistoryPartialRefundDto> GetByPaymentHistoryIdAsync(int paymentHistoryId);

    /// <summary>
    /// Сохраняет данные по частичному возврату
    /// </summary>
    Task SaveAsync(PaymentHistoryPartialRefundDto dto);

    /// <summary>
    /// Возвращает данные по частичным возвратам
    /// </summary>
    /// <param name="paymentHistoryIds">Коллекция идентификаторов платежей</param>
    public Task<IReadOnlyCollection<PaymentHistoryPartialRefundDto>> GetByPaymentHistoryIdsAsync(
        IReadOnlyCollection<int> paymentHistoryIds);
}