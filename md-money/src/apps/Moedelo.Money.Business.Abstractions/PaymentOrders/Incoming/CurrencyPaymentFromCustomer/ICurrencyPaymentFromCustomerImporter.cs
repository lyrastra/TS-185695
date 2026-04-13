using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerImporter
    {
        Task ImportAsync(CurrencyPaymentFromCustomerImportRequest request);
    }
}