using System;

namespace Moedelo.Postings.Dto
{
    /// <summary>
    /// полная информация о двусторонней связи документов
    /// </summary>
    public class TwoWayLinkOfDocumentsDto
    {
        public TwoWayLinkOfDocumentsIdDto LinkId { get; set; }
        
        /// <summary>
        /// сумма связи
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Дата связи
        /// </summary>
        public DateTime Date { get; set; }
    }
}