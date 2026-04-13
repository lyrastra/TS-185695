using Moedelo.BankIntegrations.ApiClient.Dto.Acceptance.Sber;
using Moedelo.BankIntegrations.Dto.Movements;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Sber
{
    public interface ISberAdapterClient
    {
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto, CancellationToken cancellationToken);

        Task<AcceptanceAdvanceResponseDto> CreateAcceptanceAdvanceAsync(AcceptanceAdvanceRequestDto dto, CancellationToken cancellationToken);

        Task<AcceptanceAdvanceResponseDto> GetAcceptanceAdvanceAsync(int firmId, string externalId, CancellationToken cancellationToken);

        Task<AcceptanceAdvanceStateResponseDto> GetAcceptanceAdvanceStateAsync(int firmId, string externalId, CancellationToken cancellationToken);
    }
}
