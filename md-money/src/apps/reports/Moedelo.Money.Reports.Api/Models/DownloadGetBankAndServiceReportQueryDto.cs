using System;

namespace Moedelo.Money.Reports.Api.Models
{
    public class DownloadGetBankAndServiceReportQueryDto
    {
        public DateTime OnDate { get; set; }

        public string Email { get; set; }
    }
}
