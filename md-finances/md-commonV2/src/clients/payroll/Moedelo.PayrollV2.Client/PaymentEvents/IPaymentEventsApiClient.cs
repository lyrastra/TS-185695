using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Client.SalarySettings.DTO;

namespace Moedelo.PayrollV2.Client.PaymentEvents
{
    public interface IPaymentEventsApiClient : IDI
    {
        Task<byte[]> GetPaymentEventFile(int firmId, int? paymentEventFileId);

        Task<bool> DeletePaymentEventFile(int firmId, int paymentEventFileId);
    }
}