using System.Collections.Generic;

namespace Moedelo.Money.Business.Wrappers
{
    class ApiPageResponseWrapper<T>
    {
        public int TotalCount { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public IReadOnlyCollection<T> Data { get; set; }
    }
}
