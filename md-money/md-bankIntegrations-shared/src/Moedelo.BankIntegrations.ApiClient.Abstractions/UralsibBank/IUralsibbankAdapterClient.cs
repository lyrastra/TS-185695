using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.BankIntegrations.Dto.UralsibbankSso;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Uralsibbank
{
    public interface IUralsibbankAdapterClient
    {
        Task<UralsibClientInfoDto> GetClientInfoAsync(string redirectUri, string authCode);

        Task<int> SaveIntegrationDataAsync(IntegrationDataDto integrationDataDto);
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
