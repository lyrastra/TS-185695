using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class LinkedDocument
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public AccountingDocumentType Type { get; set; }

        public decimal PayedSum { get; set; }
    }
}