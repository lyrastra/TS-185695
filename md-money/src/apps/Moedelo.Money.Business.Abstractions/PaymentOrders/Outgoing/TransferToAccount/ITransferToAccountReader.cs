using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountReader : IPaymentOrderReader<TransferToAccountResponse>
    {
    }
}
