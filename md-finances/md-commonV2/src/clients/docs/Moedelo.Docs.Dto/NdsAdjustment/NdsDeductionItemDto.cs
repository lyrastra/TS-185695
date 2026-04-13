using System;

namespace Moedelo.Docs.Dto.NdsAdjustment
{
    public class NdsDeductionItemDto
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public NdsDeductionDocumentType DocumentType { get; set; }

        /// <summary>
        /// Сумма НДС документа
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Доступно НДС к вычету
        /// </summary>
        public decimal NdsDeductionAvailable { get; set; }

        /// <summary>
        /// Сумма принятого НДС к вычету
        /// </summary>
        public decimal NdsDeductionAccepted { get; set; }

        /// <summary>
        /// Документ в закрытом периоде
        /// </summary>
        public bool InClosedPeriod { get; set; }

        /// <summary>
        /// Принять к вычету можно последний квартал
        /// </summary>
        public bool IsLastQuarter { get; set; }

        /// <summary>
        /// Наименование контрагента
        /// </summary>
        public string KontragentName { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public long KontragentId { get; set; }
    }
}