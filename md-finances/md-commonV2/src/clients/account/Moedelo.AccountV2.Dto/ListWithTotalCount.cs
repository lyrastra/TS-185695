using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto
{
    public class ListWithTotalCount<T>
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}