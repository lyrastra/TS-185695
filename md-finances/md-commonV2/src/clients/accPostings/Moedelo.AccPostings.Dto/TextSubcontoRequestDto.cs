using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccPostings.Dto
{
    public class TextSubcontoRequestDto
    {
        public string Name { get; set; }

        public SubcontoType Type { get; set; }
    }
}