using Moedelo.HistoricalLogs.Enums;
using Moedelo.Money.CashOrders.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions.HistoricalLogs
{
    public interface IHistoricalLogsCommandWriter
    {
        Task WriteAsync(LogOperationType operationType, CashOrder cashOrder);
    }
}
