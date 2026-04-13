using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.PaymentHistory
{
    public interface IPaymentHistoryExApiClient : IDI
    {
        Task<PaymentHistoryExDto> GetAsync(int paymentId);
        Task<List<PaymentHistoryExDto>> GetAsync(IReadOnlyCollection<int> paymentIds);
        Task<List<PaymentHistoryExDto>> GetAsync(PaymentHistoryExRequestDto criteria);
    }
}