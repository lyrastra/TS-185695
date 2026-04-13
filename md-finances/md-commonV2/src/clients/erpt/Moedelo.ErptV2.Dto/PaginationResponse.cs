using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto
{
    public class PaginationResponse<T>
    {
        public int Total { get; set; }
        public List<T> Collection { get; set; }
    }
}