using System;

namespace Moedelo.AccountingV2.Dto.RetailReport
{
    public class RetailReportRequest
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public DateTime? AfterDate { get; set; }
        public DateTime? BeforeDate { get; set; }
    }
}