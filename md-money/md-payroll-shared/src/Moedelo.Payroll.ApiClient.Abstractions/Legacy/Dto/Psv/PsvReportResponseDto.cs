using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Psv
{
    public class PsvReportResponseDto
    {
        public List<PsvWorkerDto> Workers { get; set; }

        public List<PsvChargeDto> Charges { get; set; }
    }
}