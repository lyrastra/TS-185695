using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto
{
    public class ListWithCountDto<T>
    {
        public IList<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
