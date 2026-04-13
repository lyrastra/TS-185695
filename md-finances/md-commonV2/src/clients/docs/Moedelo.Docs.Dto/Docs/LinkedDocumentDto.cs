using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.Docs
{
    public class LinkedDocumentDto
    {
        /// <summary>
        /// Идентификатор связанного документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип связанного документа
        /// </summary>
        public AccountingDocumentType Type { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }
    }
}