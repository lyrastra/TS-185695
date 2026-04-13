using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce
{
    public class OutsourceGetProductSynonymsResponseDto
    {
        public IReadOnlyCollection<OutsourceProductSynonymDto> Synonyms { get; set; }

        public int TotalCount { get; set; }
    }
}
