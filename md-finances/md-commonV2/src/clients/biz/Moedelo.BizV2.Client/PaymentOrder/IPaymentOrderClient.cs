using System.Threading.Tasks;
using Moedelo.BizV2.Dto.PaymentOrder;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BizV2.Client.PaymentOrder
{
    public interface IPaymentOrderClient : IDI
    {
        Task<SendPaymentOrderResponseDto> SendPaymentOrderAsync(SendPaymentOrderRequestDto dto);

        Task<byte[]> GetSberbankReceiptAsync(int firmId, int userId, GetSberbankReceiptDto dto);

        Task<byte[]> DownloadPaymentAsync(int firmId, int userId, DownloadPaymentDto paymentDto);

        Task<byte[]> DownloadReceiptAsync(int firmId, int userId, DownloadPaymentDto paymentDto);
    }
}