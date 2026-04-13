using Moedelo.Money.Domain.AccPostings;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther
{
    public class CurrencyOtherOutgoingCustomAccPosting
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public IReadOnlyCollection<Subconto> DebitSubconto { get; set; }

        public int DebitCode { get; set; }

        public long CreditSubconto { get; set; }

        public string Description { get; set; }
    }
}