using Moedelo.BankIntegrations.Dto.Movements;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Sovcom
{
    public interface ISovcomBankAdapterClient
    {
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
