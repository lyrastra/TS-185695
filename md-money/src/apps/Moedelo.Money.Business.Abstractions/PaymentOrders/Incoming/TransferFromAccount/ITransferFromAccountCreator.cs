using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(TransferFromAccountSaveRequest saveRequest);
    }
}