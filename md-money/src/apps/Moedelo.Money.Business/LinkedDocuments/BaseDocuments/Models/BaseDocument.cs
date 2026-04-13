using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models
{
    class BaseDocument
    {
        public long Id { get; set; }

        public LinkedDocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}
