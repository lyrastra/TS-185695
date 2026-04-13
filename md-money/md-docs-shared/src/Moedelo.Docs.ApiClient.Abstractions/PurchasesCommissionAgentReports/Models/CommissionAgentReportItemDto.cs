using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    public class CommissionAgentReportItemDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Кол-во
        /// </summary>
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Сумма с НДС
        /// </summary>
        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Дата отгрузки/дата возврата
        /// </summary>
        public DateTime? ShipmentDate { get; set; }

        /// <summary>
        /// Документ реализации
        /// Заполняется только для возвратов
        /// </summary>
        public SalesDocumentDto SalesDocument { get; set; }
        
        /// <summary>
        /// Признак "юридическое лицо"
        /// </summary>
        public bool LegalEntity { get; set; }

        /// <summary>
        /// Тип(Код) операции
        /// </summary>
        public NdsOperationTypes? NdsOperationType { get; set; }
        
        /// <summary>
        /// Признак, указывающий учитывается ли данная позиция
        /// </summary>
        public bool Unaccounted { get; set; }
    }
}
