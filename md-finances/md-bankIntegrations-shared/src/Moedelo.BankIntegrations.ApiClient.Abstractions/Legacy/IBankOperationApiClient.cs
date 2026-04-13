using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.InitIntegration;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy
{
    public interface IBankOperationApiClient
    {
        Task<RequestMovementListResponseDto> RequestMovements(RequestMovementListRequestDto dto);

        Task<IntegrationTurnResponseDto> IntegrationTurn(IntegrationTurnRequestDto dto);

        Task<SendPaymentOrderResponseDto> SendPaymentOrdersAsync(SendPaymentOrderRequestDto dto);
    }
}
