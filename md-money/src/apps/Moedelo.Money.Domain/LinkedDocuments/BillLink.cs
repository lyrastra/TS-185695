using System;

namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class BillLink
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal BillSum { get; set; }

        public decimal PaidSum { get; set; }

        public decimal LinkSum { get; set; }
    }
}
