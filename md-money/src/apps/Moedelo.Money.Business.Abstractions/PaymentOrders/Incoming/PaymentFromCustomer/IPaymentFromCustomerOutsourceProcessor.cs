using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;

public interface IPaymentFromCustomerOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(PaymentFromCustomerSaveRequest request);
}