using Moedelo.BankIntegrations.Dto.Movements;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.NbdBank
{
    public interface INbdAdapterClient
    {
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
