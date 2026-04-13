using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.UnifiedBudgetaryPayment
{
    public interface IUnifiedBudgetaryPaymentCreator
    {
        Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request);
    }
}

