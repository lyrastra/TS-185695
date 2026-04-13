using Moedelo.BankIntegrations.ApiClient.Dto.Avangard;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Avangard
{
    public interface IAvangardAdapterClient
    {
        Task<TokenDto> GetTokenAsync(string code, string redirectUri);

        Task<int> SaveTokenDataAsync(TokenDataDto integrationDataDto);
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
