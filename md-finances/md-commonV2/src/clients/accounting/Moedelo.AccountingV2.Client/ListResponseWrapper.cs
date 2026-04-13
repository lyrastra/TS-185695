using System.Collections.Generic;

namespace Moedelo.AccountingV2.Client
{
    internal class ListResponseWrapper<T>
    {
        public List<T> Items { get; set; }
    }
}