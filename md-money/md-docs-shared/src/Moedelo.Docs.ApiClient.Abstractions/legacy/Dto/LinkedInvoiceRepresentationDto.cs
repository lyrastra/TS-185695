using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Dto
{
    public class LinkedInvoiceRepresentationDto
    {
        /// <summary>
        /// Id документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа (обязательное поле)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа (обязательное поле)
        /// </summary>
        public DateTime? DocDate { get; set; }

        /// <summary>
        /// НДС к вычету
        /// </summary>
        public List<NdsDeductionDto> NdsDeductions { get; set; }

        /// <summary>
        /// Авансовые сч-фактуры
        /// </summary>
        public List<AdvanceInvoiceDto> AdvanceInvoices { get; set; }
    }
}