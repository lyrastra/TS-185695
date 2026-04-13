using System;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Docs.Dto.PurchasesCurrencyInvoices
{
    public class PurchasesCurrencyInvoiceDto
    {
        public long DocumentBaseId { get; set; }

        public decimal Sum { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public long KontragentId { get; set; }

        public long StockId { get; set; }

        public Currency Currency { get; set; }
    }
}