using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Bills;

namespace Moedelo.AccountingV2.Dto.Bills.Simple.SalesBill
{
    /// <summary>
    /// Облегчённая модель счёта
    /// </summary>
    public class SalesBillSimpleDto
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
        /// Тип счета 1 - Обычный, 2 - Счет-договор
        /// </summary>
        public BillType Type { get; set; }

        /// <summary>
        /// Id склада
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesBillSimpleItemDto> Items { get; set; }

        /// <summary>
        /// Провести в бух. учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}
