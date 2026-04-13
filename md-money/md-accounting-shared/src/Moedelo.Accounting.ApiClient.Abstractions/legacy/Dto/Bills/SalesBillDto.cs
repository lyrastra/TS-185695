using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills
{
    /// <summary>
    /// Полная модель счета (с позициями и другой информацией) 
    /// </summary>
    public class SalesBillDto : SalesBillCollectionItemDto
    {
        /// <summary>
        /// Информация об изменениях документа
        /// </summary>
        public DocumentContextDto Context { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesBillItemDto> Items { get; set; }

        /// <summary>
        /// Ссылка на счет для партнеров
        /// </summary>
        public string Online { get; set; }

        /// <summary>
        /// Платежи, связанные со счётомStatement
        /// </summary>
        public List<LinkedPaymentDto> Payments { get; set; }

        /// <summary>
        /// Идентификатор склада
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Использовать подпись и печать
        /// </summary>
        public bool UseStampAndSign { get; set; }
    }
}