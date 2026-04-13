using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;

public interface ICurrencyBankFeeOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(CurrencyBankFeeSaveRequest request);
}