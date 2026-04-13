using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.RetailReports.Events
{
    public sealed class SalesRetailReportUpdatedMessage
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// СНО документа (при сдвоенной системе налогообложения компании) 
        /// </summary>
        public TaxationSystemType TaxationSystemType { get; set; }
        
        /// <summary>
        /// Список связанных ПКО ("на основании")
        /// </summary>
        public IReadOnlyCollection<long> CashOrderBaseIds { get; set; } = Array.Empty<long>();

        /// <summary>
        /// Позиции документа
        /// </summary>
        public IReadOnlyCollection<SalesRetailReportItem> Items { get; set; } = Array.Empty<SalesRetailReportItem>();

        /// <summary>
        /// Проведён в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Сумма ОРП. Заполняется если в учётной политике "Оценка товаров для розничной торговли" переведена в
        /// "В продажных ценах с отдельным учетом наценок (скидок) на счете 42 Торговая наценка"
        /// </summary>
        public decimal SaleSum { get; set; }

        /// <summary>
        /// Признак усечённой модели ОРП. Усечённая модель не передаёт позиции ОРП.
        /// Позиции нужно дочитывать, где они нужны
        /// </summary>
        public bool IsTruncated { get; set; }
    }
}
