using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccPostings.Dto
{
    public class SubcontoSaveRequestDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SubcontoType Type { get; set; }
    }
}