using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;

public interface ICurrencyTransferFromAccountOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(CurrencyTransferFromAccountSaveRequest request);
}