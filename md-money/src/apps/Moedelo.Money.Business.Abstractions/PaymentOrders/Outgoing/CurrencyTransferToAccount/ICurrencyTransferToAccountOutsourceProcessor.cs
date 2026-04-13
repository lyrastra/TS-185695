using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;

public interface ICurrencyTransferToAccountOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(CurrencyTransferToAccountSaveRequest request);
}