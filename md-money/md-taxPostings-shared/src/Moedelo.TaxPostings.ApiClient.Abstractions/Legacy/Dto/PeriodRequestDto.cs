using System;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto
{
    public class PeriodRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
