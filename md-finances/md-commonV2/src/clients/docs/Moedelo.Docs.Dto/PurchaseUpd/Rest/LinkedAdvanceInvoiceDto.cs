using System;

namespace Moedelo.Docs.Dto.PurchaseUpd.Rest
{
    public class LinkedAdvanceInvoiceDto
    {
        /// <summary>
        /// Идентификатор счета-фактуры
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Номер счета-фактуры
        /// </summary>
        public string Number { get; set; }
        
        /// <summary>
        /// Дата счета-фактуры
        /// </summary
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Сумма НДС к восстановлению
        /// </summary>
        public decimal Sum { get; set; }
    }
}