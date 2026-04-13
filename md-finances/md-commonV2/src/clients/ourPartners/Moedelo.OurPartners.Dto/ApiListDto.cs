using System.Collections.Generic;

namespace Moedelo.OurPartners.Dto
{
    public class ApiListDto<T>
    {
        public List<T> Data { get; set; }
        
        public int TotalCount { get; set; }
        
        public int Offset { get; set; }
        
        public int Limit { get; set; }
    }
}