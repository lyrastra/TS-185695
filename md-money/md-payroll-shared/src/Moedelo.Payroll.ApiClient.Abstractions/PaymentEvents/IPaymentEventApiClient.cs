using System.Threading.Tasks;

namespace Moedelo.Payroll.ApiClient.Abstractions.PaymentEvents;

public interface IPaymentEventApiClient
{
    Task<byte[]> GetPaymentEventFileAsync(int firmId, int paymentEventFileId);
}