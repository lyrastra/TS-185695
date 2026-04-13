using System;

namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class InvoiceLink
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }
    }
}
