using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Rshb;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Rshb;

public interface IRshbAdapterClient
{
    Task<ClientInfoResponseDto> GetClientInfoAsync(string rshbOrgId);
    Task<bool> SendConfirmAsync(SendConfirmRequestDto dto);
    Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
}