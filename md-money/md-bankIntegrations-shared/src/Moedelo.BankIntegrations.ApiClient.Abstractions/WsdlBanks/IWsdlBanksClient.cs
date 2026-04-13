using Moedelo.BankIntegrations.Dto.Movements;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.WsdlBanks
{
    public interface IWsdlBanksClient
    {
        Task<RequestMovementResponseDto> RequestMovementsAsync(RequestMovementRequestDto requestDto);
    }
}
