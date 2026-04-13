using Moedelo.BankIntegrations.ApiClient.Dto.OtpBank;
using Moedelo.BankIntegrations.Dto.Movements;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.OtpBank;

public interface IOtpAdapterClient
{
    Task<ClientInfoResponseDto> GetClientInfoAsync(string integrationId);
    Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
}
