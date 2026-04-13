using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;

public interface ITransferToAccountOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(TransferToAccountSaveRequest request);
}