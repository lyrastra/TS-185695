using Moedelo.HistoricalLogs.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs
{
    public interface IHistoricalLogsCommandWriter
    {
        Task WriteAsync(LogOperationType operationType, PaymentOrder paymentOrder);
        Task WriteActualizeAsync(IReadOnlyCollection<ActualizeRequestItem> items);
        Task WriteApprovedAsync(IReadOnlyCollection<long> baseIds);
        Task WriteOutsourceStateUpdatedAsync(int userId, IReadOnlyList<OutsourceStateUpdateResult> updated);
    }
}
