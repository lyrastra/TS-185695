using System;

namespace Moedelo.Estate.Kafka.Abstractions.Events.EventData.InventoryCard
{
    public class FixedAssetInvestmentDocumentEventData
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; set; }
        
        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType DocumentType { get; set; }
        
        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal DocumentSum { get; set; }
    }
}