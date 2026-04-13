using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;

public interface ITransferFromCashCollectionOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(TransferFromCashCollectionSaveRequest request);
}