using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.Common.Dto
{
    public class ApiPageDto<T>
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int totalCount { get; set; }
        public IReadOnlyCollection<T> data { get; set; }
    }
}