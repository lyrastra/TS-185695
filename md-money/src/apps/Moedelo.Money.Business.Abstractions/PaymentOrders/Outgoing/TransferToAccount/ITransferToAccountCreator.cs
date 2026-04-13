using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(TransferToAccountSaveRequest saveRequest);
        Task<TransferToAccountSaveResponse> CreateWithIncomingAsync(TransferToAccountSaveRequest saveRequest);
    }
}