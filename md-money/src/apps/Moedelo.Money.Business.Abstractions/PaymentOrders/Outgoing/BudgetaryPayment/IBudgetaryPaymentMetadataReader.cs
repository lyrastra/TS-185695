using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentMetadataReader
    {
        Task<BudgetaryPaymentMetadata> GetAsync(DateTime paymentDate/*BudgetaryKbkRequest request*/);
    }
}
