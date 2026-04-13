using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccPostings.Dto
{
    public class SyntheticAccountSubcontoRuleDto
    {
        public long AccountId { get; set; }

        public SubcontoType SubcontoType { get; set; }

        public int Level { get; set; }
    }
}
