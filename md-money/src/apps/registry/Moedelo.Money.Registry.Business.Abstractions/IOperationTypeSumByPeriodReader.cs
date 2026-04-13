using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.Business.Abstractions;

public interface IOperationTypeSumByPeriodReader
{
    Task<IReadOnlyList<OperationTypeSumByPeriodResult>> GetAsync(OperationTypeSumByPeriodRequest request, CancellationToken cancellationToken);
}
