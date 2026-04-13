using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages
{
    public class CommissionAgentReportItemMessage
    {
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
        /// Идентификатор отчёта комиссионера, по которому возврат.
        /// Заполняется только для возвратов
        /// </summary>
        public long? RefundCommissionAgentReportId { get; set; }

        /// <summary>
        /// Признак "юридическое лицо"
        /// </summary>
        public bool LegalEntity { get; set; }
        
        /// <summary>
        /// Тип(Код) операции
        /// </summary>
        public NdsOperationTypes? NdsOperationType { get; set; }
        
        /// Признак, указывающий учитывается ли данная позиция
        /// </summary>
        public bool Unaccounted { get; set; }
    }
}