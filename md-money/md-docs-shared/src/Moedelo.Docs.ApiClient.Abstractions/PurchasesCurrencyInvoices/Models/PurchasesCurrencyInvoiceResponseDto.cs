using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCurrencyInvoices.Models
{
    public class PurchasesCurrencyInvoiceResponseDto
    {
        public long DocumentBaseId { get; set; }

        public decimal Sum { get; set; }

        public DateTime DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public long KontragentId { get; set; }

        public long StockId { get; set; }

        /// <summary>
        /// Валюта. Source enum: https://github.com/moedelo/md-requisites-shared/blob/bdc2f35e7fb9d0e6e61871782a4cb474be5dc46c/src/Moedelo.Requisites.Enums/SettlementAccounts/Currency.c 
        /// </summary>
        public int Currency { get; set; }
        
        // Дата принятия рисков
        public DateTime AccountingDate { get; set; }
        
        // Страна ЕАЭС
        public bool IsEaeunionCountry { get; set; }
        
        public IReadOnlyCollection<PurchaseCurrencyInvoiceItemResponseDto> Items { get; set; }
        
        public PurchasesCurrencyInvoicePaymentLinkDto BudgetaryPayment { get; set; }
        
        public IReadOnlyCollection<PurchasesCurrencyInvoicePaymentLinkDto> CurrencyPayments { get; set; }
    }
}