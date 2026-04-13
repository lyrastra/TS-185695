using System.Collections.Generic;

namespace Moedelo.StockV2.Client.ResponseWrappers
{
    internal class ListResponse<T>
    {
        public ListResponse()
        {
            Items = new List<T>();
        }

        public ListResponse(List<T> items)
        {
            Items = items;
        }

        public List<T> Items { get; set; }
    }
}