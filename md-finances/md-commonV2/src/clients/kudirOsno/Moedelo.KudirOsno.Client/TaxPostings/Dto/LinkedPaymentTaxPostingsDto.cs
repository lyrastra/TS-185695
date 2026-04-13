using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;

namespace Moedelo.KudirOsno.Client.TaxPostings.Dto
{
    /// <summary>
    /// Информация и проводки связанного платежа
    /// </summary>
    public class LinkedPaymentTaxPostingsDto
    {
        /// <summary>
        /// Идентификатор связанного платежа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата связанного дукумента
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер связанного платежа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Тип связанного платежа
        /// </summary>
        public AccountingDocumentType Type { get; set; }

        /// <summary>
        /// Проводки связанного платежа
        /// </summary>
        public IReadOnlyCollection<TaxPostingDto> Postings { get; set; }
    }
}
