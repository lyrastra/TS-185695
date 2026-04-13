using System;
using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Money.Dto.CurrencyPaymentOrders
{
    public class CurrencyPaymentOrderDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public decimal TotalSum { get; set; }

        public int SettlementAccountId { get; set; }

        public MoneyDirection Direction { get; set; }

        public OperationType OperationType { get; set; }
    }
}