using System;

namespace Moedelo.Docs.Dto.SalesUpd
{
    /// <summary>
    /// УПД (Продажи): привязать/отвязать счет
    /// </summary>
    public class SalesUpdBillSaveRequestDto
    {
        /// <summary>
        /// BaseId УПД
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Счет
        /// </summary>
        public LinkedBill Bill { get; set; }
        
        /// <summary>
        /// Тип операции
        /// </summary>
        public ActionType Type { get; set; }
        
        public enum ActionType
        {
            Link = 1,
            Unlink = 2
        }

        public class LinkedBill
        {
            public long DocumentBaseId { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
        }
    }
}