using System;

namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class DocumentLink
    {
        public long DocumentBaseId { get; set; }

        public DocumentType Type { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal DocumentSum { get; set; }

        public decimal LinkSum { get; set; }

        public decimal PaidSum { get; set; }
    }
}
