using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;

public interface IPaymentToAccountablePersonOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(PaymentToAccountablePersonSaveRequest request);
}