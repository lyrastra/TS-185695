using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;

public interface IRentPaymentOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(RentPaymentSaveRequest request);
}