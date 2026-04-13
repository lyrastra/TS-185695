using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Bills.Simple.SalesInvoice
{
    /// <summary>
    /// Облегчённая модель счёта-фактуры продаж
    /// </summary>
    public class SalesInvoiceSimpleDto
    {
        /// <summary>
        /// Id документа (Сквозная нумерация по всем типам документов)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа (уникальный в пределах года)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Провести в бух. учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesInvoiceSimpleItemDto> Items { get; set; }
    }
}
