using System;

namespace Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models
{
    public class DeductibleUpdDto
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
        /// Номер документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Сумма НДС по документу
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Сумма вычета НДС
        /// </summary>
        public decimal NdsDeductionSum { get; set; }
        
        /// <summary>
        /// Доступно НДС к вычету
        /// </summary>
        public decimal NdsDeductionAvailable { get; set; }

        /// <summary>
        /// Сумма принятого НДС к вычету в закрытом периоде
        /// </summary>
        public decimal NdsDeductionSumInClosePeriod { get; set; }
        
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }
    }
}