using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outsource;

public interface IOutsourcePaymentImportService
{
    /// <summary>
    /// Данные для отчета по подтверждениям (Аутсорс - Массовая работа с клиентами - Выписки)
    /// </summary>
    Task<IReadOnlyList<ConfirmClickReportPayment>> GetAsync(IReadOnlyCollection<long> baseIds, CancellationToken ctx);

    /// <summary>
    /// Установить состояние блокировки "желтая таблица" для списка п/п
    /// </summary>
    Task UpdateOutsourceStateAsync(int userId, IReadOnlyCollection<long> baseIds, OutsourceState? state,
        CancellationToken ctx);
}