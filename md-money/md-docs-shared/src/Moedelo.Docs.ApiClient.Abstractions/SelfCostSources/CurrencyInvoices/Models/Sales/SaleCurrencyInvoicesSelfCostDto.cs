using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Sales
{
    public class SaleCurrencyInvoicesSelfCostDto
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
        public IReadOnlyCollection<SaleCurrencyInvoicesSelfCostItemDto> Items { get; set; }
    }
}