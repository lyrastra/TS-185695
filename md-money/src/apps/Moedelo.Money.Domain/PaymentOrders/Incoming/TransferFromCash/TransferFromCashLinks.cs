using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash
{
    /// <summary>
    /// Блок связей для операции "Поступление из кассы"
    /// </summary>
    public class TransferFromCashLinks
    {
        public RemoteServiceResponse<CashOrderLink> CashOrder { get; set; }
    }
}