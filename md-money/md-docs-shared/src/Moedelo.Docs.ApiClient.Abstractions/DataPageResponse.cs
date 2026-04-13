using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions
{
    public class DataPageResponse<T>
    {
        public int Offset { get; set; }
        
        public int Limit { get; set; }
        
        public int TotalCount { get; set; }
        
        public IReadOnlyCollection<T> Data { get; set; }
    }
}