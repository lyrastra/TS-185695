using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto;

namespace Moedelo.Billing.Abstractions.TruncatePayment;

public interface ITruncatePaymentApiClient
{
    Task TruncatePaymentAsync(TruncatePaymentRequestDto dto);
}