using System.Collections.Generic;

namespace Moedelo.StockV2.Dto
{
    public class ListWithCount<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}