using System;

namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class BaseDocumentCreateRequest
    {
        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }
    }
}