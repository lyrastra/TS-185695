using Moedelo.BankIntegrations.Dto.Movements;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Raiffeisen
{
    public interface IRaiffeisenBankAdapterClient
    {
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
