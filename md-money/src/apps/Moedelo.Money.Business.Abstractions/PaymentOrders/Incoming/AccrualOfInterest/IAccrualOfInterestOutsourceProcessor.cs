using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;

public interface IAccrualOfInterestOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(AccrualOfInterestSaveRequest request);
}