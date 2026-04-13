using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentHistory;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.PaymentHistory;

public interface IPaymentHistoryExV3ApiClient
{
    /// <summary>
    /// Поллучение информации о счёте по его номеру
    /// </summary>
    /// <param name="billNumber">Номер счёта</param>
    Task<PaymentHistoryExDto> GetByBillNumberAsync(
        string billNumber,
        HttpQuerySetting httpQuerySetting = null);

    Task<List<PaymentHistoryExDto>> GetByCriteriaAsync(
        PaymentHistoryExRequestDto criteriaDto,
        HttpQuerySetting setting = null);
    
    Task<PaymentHistoryExDto> GetByPaymentIdAsync(
        int paymentId, 
        HttpQuerySetting httpQuerySetting = null);
}
