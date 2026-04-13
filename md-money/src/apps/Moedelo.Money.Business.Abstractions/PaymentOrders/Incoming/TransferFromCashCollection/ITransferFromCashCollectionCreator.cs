using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection
{
    public interface ITransferFromCashCollectionCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(TransferFromCashCollectionSaveRequest saveRequest);
    }
}