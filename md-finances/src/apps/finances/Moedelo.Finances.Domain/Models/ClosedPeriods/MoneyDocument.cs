using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Domain.Models.ClosedPeriods
{
    public class MoneyDocument
    {
        /// <summary>
        /// Тип документа. ПП/РКО/ПКО 
        /// </summary>
        public AccountingDocumentType Type { get; set; }

        /// <summary>
        /// Исходящий/Входящий ПП/КО
        /// </summary>
        public PaymentDirection Direction { get; set; }

        /// <summary>
        /// Для Кассовых ордеров всегда 0
        /// Для ПП - Тип платёжного поручения
        /// </summary>
        public int DocumentType { get; set; } = 0;
        
        /// <summary>
        /// Номер ПП/РКО/ПКО
        /// </summary>
        public string Number { get; set; }
        
        /// <summary>
        /// Дата ПП/РКО/ПКО
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Сумма ПП/РКО/ПКО
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}