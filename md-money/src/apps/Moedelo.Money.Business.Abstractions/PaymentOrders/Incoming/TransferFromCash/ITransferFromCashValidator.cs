using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash
{
    public interface ITransferFromCashValidator
    {
        Task ValidateAsync(TransferFromCashSaveRequest request);
    }
}