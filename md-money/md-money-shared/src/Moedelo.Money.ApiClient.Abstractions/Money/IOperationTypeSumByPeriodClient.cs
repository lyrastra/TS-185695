using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto.OperationTypeSumByPeriod;

namespace Moedelo.Money.ApiClient.Abstractions.Money;

public interface IOperationTypeSumByPeriodClient
{
    /// <summary>
    /// Получение сумм по операциям за период сгруппированных по типу операции
    /// </summary>
    public Task<OperationTypeSumByPeriodResponseDto[]> GetAsync(OperationTypeSumByPeriodRequestDto dto, CancellationToken cancellationToken);
}
