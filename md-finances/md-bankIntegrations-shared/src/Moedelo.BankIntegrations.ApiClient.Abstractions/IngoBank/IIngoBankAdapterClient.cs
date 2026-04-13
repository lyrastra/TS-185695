using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IngoBank;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.IngoBank;

public interface IIngoBankAdapterClient
{
    Task<TokenDto> GetTokenByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<RequestMovementResponseDto> RequestMovementListAsync(
        RequestMovementRequestDto requestDto, 
        CancellationToken cancellationToken = default);
}