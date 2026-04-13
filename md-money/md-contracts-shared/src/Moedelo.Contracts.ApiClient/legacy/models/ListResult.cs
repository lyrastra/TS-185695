using System.Collections.Generic;

namespace Moedelo.Contracts.ApiClient.legacy.models
{
    internal class ListResult<T>
    {
        public List<T> Items { get; set; }
    }
}