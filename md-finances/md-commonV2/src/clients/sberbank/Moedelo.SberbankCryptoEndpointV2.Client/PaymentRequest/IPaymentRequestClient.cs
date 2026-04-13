using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response;
using System.Threading.Tasks;

namespace Moedelo.SberbankCryptoEndpointV2.Client.PaymentRequest
{
    public interface IPaymentRequestClient : IDI
    {
        Task<SendPaymentRequestResponseDto> SendPaymentRequestAsync(SendPaymentRequestDto dto, HttpQuerySetting httpQuerySetting = null);
        Task<GetSberbankPaymentRequestsStatusResponseDto> GetPaymentRequestStatusAsync(GetPaymentRequestStatusRequestDto dto);
        Task<GetAllAdvanceAcceptancesResponseDto> GetAllAdvanceAcceptancesAsync(GetAllAdvanceAcceptancesRequestDto dto);
        Task<string> GetRequestAndResponseAllAdvanceAcceptancesAsync(GetAllAdvanceAcceptancesRequestDto dto);
    }
}