using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Public.ClientData.Autocomplete
{
    public class RefundPaymentAutocompleteItemClientData
    {
        public long Id { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }

        public AccountingDocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}