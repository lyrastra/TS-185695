using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccPostings.Dto
{
    public class CostItemGroupDto
    {
        public long Id { get; set; }

        public CostItemGroupCode Code { get; set; }

        public string Name { get; set; }

        public long SubcontoId { get; set; }
    }
}