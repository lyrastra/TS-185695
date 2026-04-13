using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;

public interface ICurrencyPaymentFromCustomerOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(CurrencyPaymentFromCustomerSaveRequest request);
}