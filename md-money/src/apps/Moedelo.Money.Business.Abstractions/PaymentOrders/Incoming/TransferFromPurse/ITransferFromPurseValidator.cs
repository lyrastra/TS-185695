using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse
{
    public interface ITransferFromPurseValidator
    {
        Task ValidateAsync(TransferFromPurseSaveRequest request);
    }
}