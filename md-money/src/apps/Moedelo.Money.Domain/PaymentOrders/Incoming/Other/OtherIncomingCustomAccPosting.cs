using Moedelo.Money.Domain.AccPostings;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.Other
{
    public class OtherIncomingCustomAccPosting
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int DebitCode => 510100;

        public long DebitSubconto { get; set; }

        public int CreditCode { get; set; }

        public IReadOnlyCollection<Subconto> CreditSubconto { get; set; }

        public string Description { get; set; }
    }
}
