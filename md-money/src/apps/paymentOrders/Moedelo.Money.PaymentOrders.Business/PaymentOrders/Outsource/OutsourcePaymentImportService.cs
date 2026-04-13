using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outsource;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outsource;

[InjectAsSingleton]
public class OutsourcePaymentImportService(
    IOutsourcePaymentImportDao dao,
    IHistoricalLogsCommandWriter historicalLogsCommandWriter) : IOutsourcePaymentImportService
{
    public Task<IReadOnlyList<ConfirmClickReportPayment>> GetAsync(IReadOnlyCollection<long> baseIds, CancellationToken ctx)
    {
        return dao.GetAsync(baseIds, ctx);
    }

    public async Task UpdateOutsourceStateAsync(
        int userId,
        IReadOnlyCollection<long> baseIds,
        OutsourceState? state,
        CancellationToken ctx)
    {
        // Без контекста, т. к.:
        // - платежи могут быть от разных фирм
        // - userId (аутсорсер) может не иметь доступа в конкретный ЛК
        var updated = await dao.UpdateOutsourceStateAsync(baseIds, state, ctx);
        await historicalLogsCommandWriter.WriteOutsourceStateUpdatedAsync(userId, updated);
    }
}