using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection
{
    public interface ITransferFromCashCollectionValidator
    {
        Task ValidateAsync(TransferFromCashCollectionSaveRequest request);
    }
}