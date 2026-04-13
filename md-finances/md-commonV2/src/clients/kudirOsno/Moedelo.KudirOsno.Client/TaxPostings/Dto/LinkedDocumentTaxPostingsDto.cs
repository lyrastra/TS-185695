using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.KudirOsno.Client.TaxPostings.Dto
{
    public class LinkedDocumentTaxPostingsDto
    {
        /// <summary>
        /// Идентификатор связанного документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата связанного документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер связанного документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Тип связанного документа
        /// </summary>
        public AccountingDocumentType Type { get; set; }

        /// <summary>
        /// Проводки связанного документа
        /// </summary>
        public IReadOnlyCollection<TaxPostingDto> Postings { get; set; }
    }
}