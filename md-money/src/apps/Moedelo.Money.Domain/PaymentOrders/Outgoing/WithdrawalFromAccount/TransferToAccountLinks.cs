using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public class WithdrawalFromAccountLinks
    {
        public RemoteServiceResponse<CashOrderLink> CashOrder { get; set; }
    }
}