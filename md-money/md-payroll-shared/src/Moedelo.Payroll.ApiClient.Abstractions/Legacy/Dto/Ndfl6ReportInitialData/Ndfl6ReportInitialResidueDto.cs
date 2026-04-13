using Moedelo.Payroll.Enums.Ndfl;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData
{
    public class Ndfl6ReportInitialResidueDto
    {
        public int WorkerId { get; set; }
        public NdflRateType NdflRate { get; set; }
        public int Code { get; set; }
        public decimal Sum { get; set; }
        public int Month { get; set; }
    }
}
