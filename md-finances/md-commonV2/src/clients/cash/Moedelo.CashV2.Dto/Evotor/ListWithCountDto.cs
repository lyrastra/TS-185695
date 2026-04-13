using System.Collections.Generic;

namespace Moedelo.CashV2.Dto.Evotor
{
    public class ListWithCountDto<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
