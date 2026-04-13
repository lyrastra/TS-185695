using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerImporter
    {
        Task ImportAsync(PaymentFromCustomerImportRequest request);
    }
}