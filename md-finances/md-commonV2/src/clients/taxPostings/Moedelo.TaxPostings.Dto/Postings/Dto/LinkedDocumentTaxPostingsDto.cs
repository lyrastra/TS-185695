using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto.Postings.Dto
{
    public class LinkedDocumentTaxPostingsDto<T> where T : ITaxPostingDto
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public AccountingDocumentType Type { get; set; }
        public IReadOnlyCollection<T> Postings { get; set; }
    }
}
