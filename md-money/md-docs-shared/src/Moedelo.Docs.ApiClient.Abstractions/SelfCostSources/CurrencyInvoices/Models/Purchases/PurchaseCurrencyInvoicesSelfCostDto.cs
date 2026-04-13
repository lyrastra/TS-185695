using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Purchases
{
    public class PurchaseCurrencyInvoicesSelfCostDto
    {
        /// <summary>
        /// Идентификатор инвойса.
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер инвойса.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата инвойса.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Список позиций инвойса.
        /// </summary>
        public IReadOnlyCollection<PurchaseCurrencyInvoicesSelfCostItemDto> Items { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal DocumentSum { get; set; }
        
        /// <summary>
        /// Страна принадлежит ЕАЭС
        /// </summary>
        public bool IsEaeunionCountry { get; set; }
    }
}