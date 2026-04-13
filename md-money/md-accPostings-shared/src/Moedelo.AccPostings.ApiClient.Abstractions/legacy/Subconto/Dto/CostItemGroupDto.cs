using Moedelo.AccPostings.Enums;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto
{
    public class CostItemGroupDto
    {
        public long Id { get; set; }

        public CostItemGroupCode Code { get; set; }

        public string Name { get; set; }

        public long SubcontoId { get; set; }
    }
}
