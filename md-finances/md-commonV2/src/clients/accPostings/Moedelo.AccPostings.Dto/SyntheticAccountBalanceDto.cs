using System;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccPostings.Dto
{
    public class SyntheticAccountBalanceDto
    {
        public SyntheticAccountCode Code { get; set; }
        
        public DateTime Date { get; set; }

        public decimal DebitBalance { get; set; }

        public decimal CreditBalance { get; set; }
    }
}