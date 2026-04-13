using Moedelo.BankIntegrations.ApiClient.Dto.PushMovements;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.PushMovements
{
    public interface IBankIntegrationPushMovementApiClient
    {
        Task SendZippedWithoutWsse(PushMovementRequestDto dto);

        Task SendZippedWithCreateIntegratedRequest(PushMovementRequestDto dto);
    }
}
