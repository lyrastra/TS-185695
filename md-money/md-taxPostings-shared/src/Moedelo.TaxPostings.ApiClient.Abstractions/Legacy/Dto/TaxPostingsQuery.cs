using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto
{
    public class TaxPostingsQuery
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}