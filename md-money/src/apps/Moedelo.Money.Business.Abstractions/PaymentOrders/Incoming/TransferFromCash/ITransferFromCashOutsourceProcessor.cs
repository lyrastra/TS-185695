using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;

public interface ITransferFromCashOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(TransferFromCashSaveRequest request);
}