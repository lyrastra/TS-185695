using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;

public interface IRefundFromAccountablePersonOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(RefundFromAccountablePersonSaveRequest request);
}