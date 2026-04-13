using Moedelo.AccPostings.Enums;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto
{
    public class SubcontoDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SubcontoType Type { get; set; }
    }
}
