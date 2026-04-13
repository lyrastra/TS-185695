using System;

namespace Moedelo.Docs.Dto.Ukd
{
    public class UkdCriteriaTableItemDto
    {
        /// <summary>
        /// Идентификатор УКД
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public KontragentDto Kontragent { get; set; }

        /// <summary>
        /// Учитывать в учёте
        /// </summary>
        public bool ProvideInAccounting {get; set; }

        /// <summary>
        /// Сумма УКД
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Склад из документа источника УКД
        /// </summary>
        public long? StockId { get; set; }
    }
}