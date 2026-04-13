using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.Registry.DataAccess.Abstractions.OperationTypeSumByPeriod;
using Moedelo.Money.Registry.DataAccess.OperationTypeSumByPeriod.DbModels;
using Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.DataAccess.OperationTypeSumByPeriod;

[InjectAsSingleton(typeof(IOperationTypeSumByPeriodDao))]
internal class OperationTypeSumByPeriodDao : MoedeloSqlDbExecutorBase, IOperationTypeSumByPeriodDao
{
    private readonly ISqlScriptReader scriptReader;

    public OperationTypeSumByPeriodDao(
        ISettingRepository settings,
        ISqlScriptReader scriptReader,
        ISqlDbExecutor dbExecutor,
        IAuditTracer auditTracer)
        : base(dbExecutor, settings.GetReportsReadOnlyConnectionString(), auditTracer)
    {
        this.scriptReader = scriptReader;
    }

    public async Task<IReadOnlyList<OperationTypeSumByPeriodResult>> GetAsync(int firmId, OperationTypeSumByPeriodRequest request, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var sqlTemplate = scriptReader.Get(this, "OperationTypeSumByPeriod.Scripts.Get.sql");
        var queryObject = OperationTypeSumByPeriodBuilder.Get(sqlTemplate, firmId, request);
        var dbModels = await QueryAsync<OperationTypeSumByPeriodDbModel>(queryObject, cancellationToken: ct);

        return OperationTypeSumByPeriodMapper.ToDomain(dbModels);
    }
}
