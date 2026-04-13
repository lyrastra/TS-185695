using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;

public interface ITransferFromAccountOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(TransferFromAccountSaveRequest request);
}