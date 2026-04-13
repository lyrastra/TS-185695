using System;
using System.Collections.Generic;

namespace Moedelo.Money.Reports.DataAccess.Abstractions.Balances.Models
{
    public class SettlementAccountBalancesRequest
    {
        public DateTime OnDate { get; set; }
        public IReadOnlyCollection<FirmInitDate> FirmInitDates { get; set; }
    }

    public class FirmInitDate
    {
        public int FirmId { get; set; }
        public DateTime InitDate { get; set; }
    }
}
