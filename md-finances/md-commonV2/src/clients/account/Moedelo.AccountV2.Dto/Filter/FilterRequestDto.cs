using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.Filter
{
    public class FilterRequestDto<TEnumName>
    {
        public int Offset { get; set; }

        public int? Count { get; set; }

        public ISet<SortDto<TEnumName>> Sort { get; set; }

        public ISet<FilterDto<TEnumName>> Filter { get; set; }
    }
}