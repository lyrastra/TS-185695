using System;

namespace Moedelo.AccountingV2.Dto.RetailReport
{
    public class RetailReportPaginationRequest
    {
        public long Offset { get; set; }
        public long PageSize { get; set; }
        public DateTime? AfterDate { get; set; }
        public DateTime? BeforeDate { get; set; }
    }
}