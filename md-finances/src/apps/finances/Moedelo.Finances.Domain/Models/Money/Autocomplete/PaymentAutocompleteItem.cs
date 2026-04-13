using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Domain.Models.Money.Autocomplete
{
    public class PaymentAutocompleteItem
    {
        public long DocumentBaseId { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int TotalCount { get; set; }
    }
}
