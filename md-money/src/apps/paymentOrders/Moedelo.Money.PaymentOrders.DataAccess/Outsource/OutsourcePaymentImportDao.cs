using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.DbExecutors;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.DataAccess.Outsource;

[InjectAsSingleton]
internal sealed class OutsourcePaymentImportDao : IOutsourcePaymentImportDao
{
    private readonly ISqlScriptReader scriptReader;
    private readonly IMoedeloReadOnly02DbExecutor readOnly02DbExecutor;
    private readonly IMoedeloDbExecutor moedeloDbExecutor;

    public OutsourcePaymentImportDao(
        ISqlScriptReader scriptReader,
        IMoedeloReadOnly02DbExecutor readOnly02DbExecutor,
        IMoedeloDbExecutor moedeloDbExecutor)
    {
        this.scriptReader = scriptReader;
        this.readOnly02DbExecutor = readOnly02DbExecutor;
        this.moedeloDbExecutor = moedeloDbExecutor;
    }

    public Task<IReadOnlyList<ConfirmClickReportPayment>> GetAsync(IReadOnlyCollection<long> baseIds, CancellationToken ctx)
    {
        var sql = scriptReader.Get(this, "Outsource.Scripts.GetForOutsourceConfirmClickReport.sql");
        var tempTable = baseIds.ToTempBigIntIds("BaseIds");
        var queryObject = new QueryObject(sql, null, tempTable);
        return readOnly02DbExecutor.QueryAsync<ConfirmClickReportPayment>(queryObject, cancellationToken: ctx);
    }

    public Task<IReadOnlyList<OutsourceStateUpdateResult>> UpdateOutsourceStateAsync(IReadOnlyCollection<long> baseIds, OutsourceState? state, CancellationToken ctx)
    {
        var sql = scriptReader.Get(this, "Outsource.Scripts.UpdateOutsourceStateByBaseIds.sql");
        var tempTable = baseIds.ToTempBigIntIds("BaseIds");
        var queryObject = new QueryObject(sql, new { state = (byte?)state }, tempTable);
        return moedeloDbExecutor.QueryAsync<OutsourceStateUpdateResult>(queryObject, cancellationToken: ctx);
    }
}