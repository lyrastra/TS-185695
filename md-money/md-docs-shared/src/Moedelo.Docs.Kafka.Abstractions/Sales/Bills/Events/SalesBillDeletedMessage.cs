using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Bills.Events
{
    public sealed class SalesBillDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }
    }
}
