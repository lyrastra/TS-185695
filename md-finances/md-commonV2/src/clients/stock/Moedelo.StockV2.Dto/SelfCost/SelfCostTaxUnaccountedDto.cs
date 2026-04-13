using System;

namespace Moedelo.StockV2.Dto.SelfCost
{
    /// <summary>
    /// Операция над товаром с данными о неучтенной в НУ сумме себестоимости
    /// </summary>
    public class SelfCostTaxUnaccountedDto
    {
        /// <summary>
        /// Идентификатор документа-списания
        /// </summary>
        public long SourceDocumentId { get; set; }

        /// <summary>
        /// Идентификатор StockOperationOverProduct в качестве идентификатора позиции документа
        /// </summary>
        public long OperationOverProductId { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Cумма операции над товаром (полная себестомость)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// По какому товару не учтена сумма (в случае продажи комплекта идентификатор составляющей) 
        /// </summary>
        public long UnaccountedTaxProductId { get; set; }

        /// <summary>
        /// Недосписанная сумма себестомости
        /// </summary>
        public decimal UnaccountedTaxSum { get; set; }
    }
}