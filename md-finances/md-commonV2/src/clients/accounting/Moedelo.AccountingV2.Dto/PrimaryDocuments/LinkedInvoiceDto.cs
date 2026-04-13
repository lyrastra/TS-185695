using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class LinkedInvoiceDto
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
        /// Список авансовых счет-фактур
        /// </summary>
        public List<AdvanceInvoiceDto> AdvanceInvoices { get; set; }
    }
}
