using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccPostings.Dto
{
    public class SyntheticAccountTypeDto
    {
        public long Id { get; set; }

        public SyntheticAccountCode Code { get; set; }

        public string Name { get; set; }

        public SyntheticAccountBalanceType BalanceType { get; set; }
    }
}
