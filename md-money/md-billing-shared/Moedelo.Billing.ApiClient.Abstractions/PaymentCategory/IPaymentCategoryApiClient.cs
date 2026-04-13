using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentCategory;

namespace Moedelo.Billing.Abstractions.PaymentCategory;

public interface IPaymentCategoryApiClient
{
    /// <summary>
    /// Получение примечаний к платежам
    /// </summary>
    /// <param name="paymentHistoryIds">Коллекция платежей</param>
    /// <returns>Коллекция dtos</returns>
    Task<IReadOnlyCollection<PaymentCategoryDto>> GetByPaymentHistoryIdsAsync(IReadOnlyCollection<int> paymentHistoryIds);

    /// <summary>
    /// Добавить новое примечание в бд
    /// </summary>
    /// <param name="dto">Данные для вставки</param>
    Task InsertNoteAsync(PaymentCategoryDto dto);
}