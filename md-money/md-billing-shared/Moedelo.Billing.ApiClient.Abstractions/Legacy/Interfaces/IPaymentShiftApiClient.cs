using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces;

public interface IPaymentShiftApiClient
{
    Task ShiftPaymentAsync(PaymentShiftRequestDto dto);
}