using System;

namespace Moedelo.OfficeV2.Dto.Egr
{
    public class QueryStatRequestDto
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string Query { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public byte OrderBy { get; set; }

        public bool Desc { get; set; }
    }
}
