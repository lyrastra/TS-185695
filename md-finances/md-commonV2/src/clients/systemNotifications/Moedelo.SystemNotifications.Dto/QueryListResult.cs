using System.Collections.Generic;

namespace Moedelo.SystemNotifications.Dto
{
    public class QueryListResult<T>
    {
        public IReadOnlyCollection<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
