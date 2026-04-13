using System.Collections.Generic;

namespace Moedelo.HomeV2.Client
{
    public class ListWrapper<T> where T : class, new()
    {
        public List<T> Items { get; set; }
    }
}
