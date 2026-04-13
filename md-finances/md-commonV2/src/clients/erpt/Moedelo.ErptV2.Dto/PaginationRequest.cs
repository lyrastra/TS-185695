using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.ErptV2.Dto
{
    public class PaginationRequest
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public Dictionary<string, string> Filter { get; set; }
        public Dictionary<string, SortOrder> SortBy { get; set; }
    }
}
