using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders
{
    public class PaymentOrderStatus
    {
        public long DocumentBaseId { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
    }
}