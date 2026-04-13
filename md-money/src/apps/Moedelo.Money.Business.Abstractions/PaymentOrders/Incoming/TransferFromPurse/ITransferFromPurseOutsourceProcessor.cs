using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;

public interface ITransferFromPurseOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(TransferFromPurseSaveRequest request);
}