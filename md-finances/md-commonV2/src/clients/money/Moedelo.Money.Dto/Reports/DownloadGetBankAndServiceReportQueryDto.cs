using System;

namespace Moedelo.Money.Dto.Reports
{
    public class DownloadGetBankAndServiceReportQueryDto
    {
        public DateTime OnDate { get; set; }

        public string Email { get; set; }
    }
}
