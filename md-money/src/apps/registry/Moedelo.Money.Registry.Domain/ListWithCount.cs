using System.Collections.Generic;

namespace Moedelo.Money.Registry.Domain
{
    public class ListWithCount<T>
    {
        public T[] Items { get; set; }

        public int TotalCount { get; set; }
    }
}
