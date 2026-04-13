using System.Threading.Tasks;
using Moedelo.BillingV2.Dto;
using Moedelo.BillingV2.Dto.PaymentShift;

namespace Moedelo.BillingV2.Client.PaymentShiftApi
{
    public interface IPaymentShiftApiClient
    {
        Task ShiftPaymentAsync(PaymentShiftRequestDto dto);
        Task<BaseDto> ShiftSuccessBackofficeBillingTrial(BackofficeBillingShiftRequestDto dto);
    }
}
