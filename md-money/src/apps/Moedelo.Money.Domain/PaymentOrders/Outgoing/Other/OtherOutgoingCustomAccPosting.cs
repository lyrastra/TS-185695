using System;
using System.Collections.Generic;
using Moedelo.Money.Domain.AccPostings;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Other
{
    public class OtherOutgoingCustomAccPosting
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int DebitCode { get; set; }

        public IReadOnlyCollection<Subconto> DebitSubconto { get; set; }

        public int CreditCode => 510100;

        public long CreditSubconto { get; set; }

        public string Description { get; set; }
    }
}
