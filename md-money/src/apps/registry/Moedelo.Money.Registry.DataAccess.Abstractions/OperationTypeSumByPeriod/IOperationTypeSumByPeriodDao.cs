using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.DataAccess.Abstractions.OperationTypeSumByPeriod;

public interface IOperationTypeSumByPeriodDao
{
    /// <summary>
    /// Возвращает суммы операций сгрупированные по типу и периоду
    /// </summary>
    Task<IReadOnlyList<OperationTypeSumByPeriodResult>> GetAsync(int firmId, OperationTypeSumByPeriodRequest request, CancellationToken cancellationToken);
}
