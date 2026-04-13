using System;
using System.Collections.Generic;

namespace Moedelo.BackofficeV2.Dto.Reports
{
    public class ReportSales2ParamDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<int> PriceListIds { get; set; }

        public bool IsIp { get; set; }

        public bool IsOoo { get; set; }

        public bool SalerByPayment { get; set; }

        public bool IsNew { get; set; }

        // куда отправляется письмо с отчётом
        public string Email { get; set; }

        public Guid QueryGuid { get; set; }
    }
}
