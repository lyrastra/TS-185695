using System;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.AccountingV2.Client.RetailRevenue.Dto
{
    public class CashierPaginationCriterions
    {
        public long CashierId { get; set; }

        public long Offset { get; set; }

        public long PageSize { get; set; }

        public DateTime? AfterDate { get; set; }

        public DateTime? BeforeDate { get; set; }

        public OperationType? OperationType { get; set; }

        public string MoneyOperationType { get; set; }
    }
}