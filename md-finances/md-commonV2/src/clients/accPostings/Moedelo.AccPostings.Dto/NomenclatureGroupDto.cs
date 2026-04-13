using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccPostings.Dto
{
    public class NomenclatureGroupDto
    {
        public long Id { get; set; }

        public NomenclatureGroupCode Code { get; set; }

        public string Name { get; set; }

        public long SubcontoId { get; set; }
    }
}