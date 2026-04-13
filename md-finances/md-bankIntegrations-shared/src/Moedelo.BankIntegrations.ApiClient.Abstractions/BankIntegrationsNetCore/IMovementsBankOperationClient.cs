using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore
{
    public interface IMovementsBankOperationClient
    {
        Task<RequestMovementListResponseDto> RequestMovementsAsync(
            RequestMovementListRequestDto dto,
            CancellationToken cancellationToken = default);
    }
}
