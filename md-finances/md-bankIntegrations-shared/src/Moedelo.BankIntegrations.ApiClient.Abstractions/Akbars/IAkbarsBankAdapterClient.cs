using Moedelo.BankIntegrations.ApiClient.Dto.Avangard;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Akbars
{
    public interface IAkbarsBankAdapterClient
    {
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
    }
}
