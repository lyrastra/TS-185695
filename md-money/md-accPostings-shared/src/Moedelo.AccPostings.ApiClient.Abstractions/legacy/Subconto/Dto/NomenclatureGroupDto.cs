using Moedelo.AccPostings.Enums;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto
{
    public class NomenclatureGroupDto
    {
        public long Id { get; set; }

        public NomenclatureGroupCode Code { get; set; }

        public string Name { get; set; }

        public long SubcontoId { get; set; }
    }
}
