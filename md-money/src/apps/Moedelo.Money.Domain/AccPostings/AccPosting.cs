using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.AccPostings
{
    public class AccPosting
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int DebitCode { get; set; }

        public IReadOnlyCollection<Subconto> DebitSubconto { get; set; }

        public int CreditCode { get; set; }

        public IReadOnlyCollection<Subconto> CreditSubconto { get; set; }

        public string Description { get; set; }
    }
}
