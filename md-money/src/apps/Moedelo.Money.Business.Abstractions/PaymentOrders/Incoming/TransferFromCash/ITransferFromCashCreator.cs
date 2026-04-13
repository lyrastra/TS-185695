using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash
{
    public interface ITransferFromCashCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(TransferFromCashSaveRequest saveRequest);
    }
}