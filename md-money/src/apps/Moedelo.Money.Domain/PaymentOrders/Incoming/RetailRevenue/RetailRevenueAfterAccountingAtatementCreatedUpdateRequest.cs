using System;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue
{
    public class RetailRevenueAfterAccountingAtatementCreatedUpdateRequest
    {
        public long DocumentBaseId { get; set; }

        public long AccountingStatementBaseId { get; set; }

        public DateTime AccountingStatementDate { get; set; }

        public decimal AccountingStatementSum { get; set; }
    }
}
