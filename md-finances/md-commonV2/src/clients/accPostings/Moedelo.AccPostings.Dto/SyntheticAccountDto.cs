using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccPostings.Dto
{
    public class SyntheticAccountDto
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public SyntheticAccountCode Code { get; set; }

        public int Level { get; set; }

        public long? ParentId { get; set; }

        public string Name { get; set; }
        
        public int Direction { get; set; }

        public SyntheticAccountBalanceType BalanceType { get; set; }

        public bool IsActual { get; set; }

        public bool HasBalance { get; set; }
    }
}
