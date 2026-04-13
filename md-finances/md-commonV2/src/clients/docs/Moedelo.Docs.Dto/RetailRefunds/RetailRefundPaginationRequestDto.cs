using System;

namespace Moedelo.Docs.Dto.RetailRefunds
{
    public class RetailRefundPaginationRequestDto
    {
        public uint Offset { get; set; }
        public uint PageSize { get; set; }
        public DateTime? AfterDate { get; set; }
        public DateTime? BeforeDate { get; set; }
    }
}