using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    /// <summary>
    /// Документ реализации. Используется в позициях вовзвратах.
    /// Сейчас в качестве документа реализации используются отчеты комиссионера
    /// </summary>
    public class SalesDocumentDto
    {
        /// <summary>
        /// Идентификатор отчета комиссионера
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер отчета комиссионера
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата отчета комиссионера
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Идентификатор возвращаемого товара
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Доступность товара в документе реализации в разрезе НДС
        /// </summary>
        public IReadOnlyList<SalesDocumentNdsItemResponseDto> AvailableItems { get; set; }
    }
}
