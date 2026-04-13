using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Alfa;
using Moedelo.BankIntegrations.ApiClient.Dto.Common;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Alfa;

public interface IAlfaAdapterClient
{
    Task<OpenIdGetTokenResponseDto> RequestTokensAsync(RequestTokensRequestDto requestDto);
    
    Task<ClientInfoResponseDto> GetClientInfoAsync(string accessToken);
    
    Task<UserInfoResponseDto> GetUserInfoAsync(string accessToken);
    
    Task<RequestMovementResponseDto> RequestMovementsAsync(RequestMovementRequestDto requestDto);

    Task<List<AccountResponseDto>> ValidAccountsAsync(ValidAccountsRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<bool> UpdateIntegrationDataAsync(IntegrationDataRequestDto requestDto, CancellationToken cancellationToken = default);
}