using System;

namespace Moedelo.CheckVerification.Client.Receipts.Models
{
    public class ReceiptRequestDto
    {
        /// <summary>
        /// ФН (Номер фискального накопителя)
        /// </summary>
        public string FnNumber { get; set; }
        
        /// <summary>
        /// Тип чека
        /// </summary>
        public CheckTypeDto Type { get; set; } 
        
        /// <summary>
        /// ФД (Номер фискального документа)
        /// </summary>
        public string FdNumber { get; set; }
        
        /// <summary>
        /// ФПД (Фискальный признак)
        /// </summary>
        public string FiscalSign { get; set; }
        
        /// <summary>
        /// Дата чека
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Сумма чека
        /// </summary>
        public decimal Sum { get; set; }
    }
}