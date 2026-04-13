using System.Collections.Generic;

namespace Moedelo.Docs.Dto.RetailRefunds
{
    public class RetailRefundPageDto
    {
        public List<RetailRefundBaseInfoDto> Items { get; set; }

        public uint StartIndex { get; set; }

        public uint PageCapacity { get; set; }

        public int TotalCount { get; set; }
    }
}