using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Money.Registry.DataAccess.Abstractions.OperationTypeSumByPeriod;
using Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.Business.OperationTypeSumByPeriod;

[InjectAsSingleton(typeof(IOperationTypeSumByPeriodReader))]
internal class OperationTypeSumByPeriodReader : IOperationTypeSumByPeriodReader
{
    private readonly IExecutionInfoContextAccessor executionInfoContext;
    private readonly IOperationTypeSumByPeriodDao dao;

    public OperationTypeSumByPeriodReader(
        IExecutionInfoContextAccessor executionInfoContext,
        IOperationTypeSumByPeriodDao dao)
    {
        this.executionInfoContext = executionInfoContext;
        this.dao = dao;
    }

    public async Task<IReadOnlyList<OperationTypeSumByPeriodResult>> GetAsync(OperationTypeSumByPeriodRequest request, CancellationToken ct)
    {
        var context = executionInfoContext.ExecutionInfoContext;
        var results = await dao.GetAsync((int)context.FirmId, request, ct);

        if (request.OperationTypes == null || request.OperationTypes.Length == 0)
        {
            return results;
        }

        return results
            .Where(r => request.OperationTypes.Contains(r.Type))
            .ToArray();
    }
}
