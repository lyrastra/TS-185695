using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Vneshtorg;

public interface IVneshtorgBankAdapterClient
{
    Task<RequestMovementResponseDto> RequestMovementListAsync(
        RequestMovementRequestDto requestDto, 
        CancellationToken cancellationToken = default);
}