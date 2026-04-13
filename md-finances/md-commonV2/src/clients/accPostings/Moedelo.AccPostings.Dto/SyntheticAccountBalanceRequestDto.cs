using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccPostings.Dto
{
    public class SyntheticAccountBalanceRequestDto
    {
        public DateTime OnDate { get; set; }
        public IReadOnlyCollection<SyntheticAccountCode> AccountCodes { get; set; }
        public DateTime BalanceDate { get; set; }
        public IReadOnlyCollection<long> SubcontoIds { get; set; }
    }
}