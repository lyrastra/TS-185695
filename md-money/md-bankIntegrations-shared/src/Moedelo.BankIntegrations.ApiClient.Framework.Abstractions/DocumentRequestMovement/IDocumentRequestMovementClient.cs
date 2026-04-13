using Moedelo.BankIntegrations.ApiClient.Dto.MovementHash;
using Moedelo.BankIntegrations.ApiClient.Dto.MovementHashService;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.DocumentRequestMovement
{
    public interface IDocumentRequestMovementClient: IDI
    {
        Task<RemoveDuplicateDocumentsResponseDto> RemoveDuplicateDocumentsAsync(RemoveDuplicateDocumentsRequestDto requestDto, int firmId, int partnerId);
        Task HashRequestsMovementsSaveAsync(List<MovementHashDto> movementHashDtos);
    }
}
