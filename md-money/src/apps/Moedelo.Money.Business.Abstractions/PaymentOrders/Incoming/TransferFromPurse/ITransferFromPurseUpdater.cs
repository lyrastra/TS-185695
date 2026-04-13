using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse
{
    public interface ITransferFromPurseUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(TransferFromPurseSaveRequest saveRequest);
    }
}