using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class MainMoneyMultiCurrencyTableSummary
    {
        public decimal StartBalance { get; set; }
        public decimal EndBalance { get; set; }

        public int IncomingCount { get; set; }
        public decimal IncomingBalance { get; set; }
        public DateTime IncomingDate { get; set; }

        public int OutgoingCount { get; set; }
        public decimal OutgoingBalance { get; set; }
        public DateTime OutgoingDate { get; set; }

        public bool HasOperations { get; set; }

        public int TotalCount { get; set; }
        public IReadOnlyCollection<MainMoneyTableOperation> Operations { get; set; }
        public Currency Currency { get; set; }
    }
}