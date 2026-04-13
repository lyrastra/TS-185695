using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.SdmBank;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.SdmBank
{
    public interface ISdmAdapterClient
    {
        Task<TokenDto> GetTokenAsync(string code);
        
        Task<int> SaveTokenDataAsync(IntegrationDataDto integrationDataDto);

        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}