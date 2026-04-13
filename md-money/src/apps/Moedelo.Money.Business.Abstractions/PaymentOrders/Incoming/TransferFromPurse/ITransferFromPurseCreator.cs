using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse
{
    public interface ITransferFromPurseCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(TransferFromPurseSaveRequest saveRequest);
    }
}