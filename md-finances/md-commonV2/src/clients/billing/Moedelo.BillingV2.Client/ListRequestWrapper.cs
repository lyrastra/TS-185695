using System.Collections.Generic;

namespace Moedelo.BillingV2.Client
{
    internal class ListRequestWrapper<T>
    {
        public IList<T> Items { get; set; }
    }
}