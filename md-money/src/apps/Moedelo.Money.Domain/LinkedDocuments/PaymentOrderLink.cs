using System;

namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class PaymentOrderLink
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }
    }
}
