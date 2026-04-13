using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;

public interface IBankFeeOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(BankFeeSaveRequest request);
}