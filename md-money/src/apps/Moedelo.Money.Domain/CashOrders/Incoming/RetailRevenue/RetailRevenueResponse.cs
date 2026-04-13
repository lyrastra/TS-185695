using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue
{
    public class RetailRevenueResponse
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public long CashId { get; set; }
        public string Number { get; set; }
        public Employee Employee { get; set; }
        public string Destination { get; set; }
        public string Comments { get; set; }
        public decimal Sum { get; set; }
        public decimal? PaidCardSum { get; set; }
        public TaxationSystemType? TaxationSystemType { get; set; }
        public long? SyntheticAccountTypeId { get; set; }
        public string ZReportNumber { get; set; }
        public long? PatentId { get; set; }

        public NdsType? NdsType { get; set; }
        public decimal? NdsSum { get; set; }
        public bool IncludeNds { get; set; }

        public bool ProvideInAccounting { get; set; }
        public ProvidePostingType PostingsAndTaxMode { get; set; }
        public ProvidePostingType TaxPostingType { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
