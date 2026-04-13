using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;

public interface IOtherIncomingOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(OtherIncomingSaveRequest request);
}