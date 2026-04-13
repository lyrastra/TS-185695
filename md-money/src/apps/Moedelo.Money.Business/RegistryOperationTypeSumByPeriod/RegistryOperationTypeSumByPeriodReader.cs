using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.RegistryOperationTypeSumByPeriod;
using Moedelo.Money.Domain.Registry.OperationTypeSumByPeriod;

namespace Moedelo.Money.Business.RegistryOperationTypeSumByPeriod;

[InjectAsSingleton(typeof(IRegistryOperationTypeSumByPeriodReader))]
internal sealed class RegistryOperationTypeSumByPeriodReader(
    IRegistryOperationTypeSumByPeriodApiClient apiClient) : IRegistryOperationTypeSumByPeriodReader
{
    public Task<OperationTypeSumByPeriodResponse[]> GetAsync(OperationTypeSumByPeriodRequest request, CancellationToken ct)
    {
        return apiClient.GetAsync(request, ct);
    }
}
