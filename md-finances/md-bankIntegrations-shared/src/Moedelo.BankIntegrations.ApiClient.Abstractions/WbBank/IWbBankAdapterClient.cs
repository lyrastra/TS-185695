using Moedelo.BankIntegrations.ApiClient.Dto.WbBank;
using Moedelo.BankIntegrations.Dto.Movements;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.WbBank;

public interface IWbBankAdapterClient
{
    Task<RequestMovementResponseDto> RequestMovementListAsync(
        RequestMovementRequestDto requestDto,
        CancellationToken cancellationToken = default);

    Task<TokenResponseDto> GetTokenByCodeAsync(
        string code,
        string redirectUri,
        CancellationToken cancellationToken = default);

    Task<ClientInfoResponseDto> GetClientInfoAsync(
        string accessToken,
        CancellationToken cancellationToken = default);
}
