using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions;

/// <summary>
/// Для функционала "Массовая работа с банковскими операциями"
/// </summary>
public interface IOutsourcePaymentImportDao
{
    Task<IReadOnlyList<ConfirmClickReportPayment>> GetAsync(IReadOnlyCollection<long> baseIds, CancellationToken ctx);

    /// <summary>
    /// Массовое обновление OutsourceState
    /// </summary>
    /// <returns>Возвращает BaseId обновленных строк</returns>
    Task<IReadOnlyList<OutsourceStateUpdateResult>> UpdateOutsourceStateAsync(IReadOnlyCollection<long> baseIds, OutsourceState? state, CancellationToken ctx);
}