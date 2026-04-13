using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Domain.Registry.OperationTypeSumByPeriod;

namespace Moedelo.Money.Business.Abstractions.RegistryOperationTypeSumByPeriod;

public interface IRegistryOperationTypeSumByPeriodReader
{
    Task<OperationTypeSumByPeriodResponse[]> GetAsync(OperationTypeSumByPeriodRequest request, CancellationToken cancellationToken);
}
