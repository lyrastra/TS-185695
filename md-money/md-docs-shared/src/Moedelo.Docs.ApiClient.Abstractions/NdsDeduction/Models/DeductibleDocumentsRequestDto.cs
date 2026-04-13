using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models
{
    public class DeductibleDocumentsRequestDto
    {
        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Конец периода
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int? KontragentId { get; set; }

        /// <summary>
        /// Принято к вычету (частично, не принято)
        /// </summary>
        public List<NdsDeductionState> NdsDeductionStates { get; set; }
    }
}