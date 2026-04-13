using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders;

/// <summary>
/// Для работы с дубликатами
/// </summary>
public interface IPaymentOrderDuplicatesApiClient
{
    /// <summary>
    /// Обновить дату операции в сервисе (не импортировать дубликат)
    /// </summary>
    Task MergeAsync(long documentBaseId, CancellationToken ctx);
}