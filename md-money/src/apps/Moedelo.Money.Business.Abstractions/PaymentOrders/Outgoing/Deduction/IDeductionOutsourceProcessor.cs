using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;

public interface IDeductionOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(DeductionSaveRequest request);
}