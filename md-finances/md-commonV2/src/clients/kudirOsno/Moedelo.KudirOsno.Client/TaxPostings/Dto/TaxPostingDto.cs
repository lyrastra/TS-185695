using System;
using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.KudirOsno.Client.TaxPostings.Dto
{
    public class TaxPostingDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public TaxPostingsDirection Direction { get; set; }
    }
}
