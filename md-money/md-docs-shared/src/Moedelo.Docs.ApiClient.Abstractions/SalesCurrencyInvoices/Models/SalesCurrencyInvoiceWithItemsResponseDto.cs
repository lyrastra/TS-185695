using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices.Models
{
    public class SalesCurrencyInvoiceWithItemsResponseDto
    {
        public long DocumentBaseId { get; set; }

        public decimal Sum { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public int KontragentId { get; set; }

        public long StockId { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Валюта расчетного счета. Source enum: https://github.com/moedelo/md-requisites-shared/blob/bdc2f35e7fb9d0e6e61871782a4cb474be5dc46c/src/Moedelo.Requisites.Enums/SettlementAccounts/Currency.c
        /// </summary>
        public int Currency { get; set; }

        public bool ProvideInAccounting { get; set; }

        public List<SalesCurrencyInvoiceItemResponseDto> Items { get; set; }
    }
}