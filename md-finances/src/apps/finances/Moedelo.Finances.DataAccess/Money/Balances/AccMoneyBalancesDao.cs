using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Balances;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Balances;

[InjectAsSingleton(typeof(IAccMoneyBalancesDao))]
internal sealed class AccMoneyBalancesDao(IMoedeloReadOnlyDbExecutor dbExecutor) : IAccMoneyBalancesDao
{
    public Task<List<MoneySourceBalance>> GetBySourceIdsAsync(int firmId, BalancesRequest request,
        CancellationToken cancellationToken)
    {
        var queryObject = AccMoneyBalancesQueryBuilder.GetBySourceIds(firmId, request)
            .WithAuditTrailSpanName("AccMoneyBalancesDao.GetBySourceIds");
        return dbExecutor.QueryAsync<MoneySourceBalance>(queryObject, cancellationToken: cancellationToken);
    }
}