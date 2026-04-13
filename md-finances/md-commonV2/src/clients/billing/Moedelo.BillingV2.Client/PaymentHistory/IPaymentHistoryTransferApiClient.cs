using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.PaymentHistory
{
    public interface IPaymentHistoryTransferApiClient : IDI
    {
        Task MovePaymentsAsync(int partnerUserId, int fromFirmId, int toFirmId);
    }
}