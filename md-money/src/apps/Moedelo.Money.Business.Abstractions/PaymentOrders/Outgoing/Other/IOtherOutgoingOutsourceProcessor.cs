using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;

public interface IOtherOutgoingOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(OtherOutgoingSaveRequest request);
}