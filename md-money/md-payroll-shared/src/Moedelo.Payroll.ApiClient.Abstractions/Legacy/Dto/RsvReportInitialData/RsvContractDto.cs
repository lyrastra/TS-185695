using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.RsvReportInitialData
{
    public class RsvContractDto
    {
        public int WorkerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsSfrCharge { get; set; }

        public bool IsTaxable { get; set; }
    }
}