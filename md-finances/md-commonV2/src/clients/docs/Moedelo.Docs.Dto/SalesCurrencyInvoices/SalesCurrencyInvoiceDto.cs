using System;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Docs.Dto.SalesCurrencyInvoices
{
    public class SalesCurrencyInvoiceDto
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