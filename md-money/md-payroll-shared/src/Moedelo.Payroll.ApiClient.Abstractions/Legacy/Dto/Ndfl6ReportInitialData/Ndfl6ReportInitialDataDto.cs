using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData
{
    public class Ndfl6ReportInitialDataDto
    {
        public List<Ndfl6ReportInitialWorkerDto> Workers { get; set; } = new List<Ndfl6ReportInitialWorkerDto>();
        public List<Ndfl6ReportInitialIncomeDto> Incomes { get; set; } = new List<Ndfl6ReportInitialIncomeDto>();
        public List<Ndfl6ReportInitialResidueDto> Residues { get; set; } = new List<Ndfl6ReportInitialResidueDto>();
        public List<Ndfl6ReportInitialResidueNoticeDto> NoticeResidues { get; set; } =
            new List<Ndfl6ReportInitialResidueNoticeDto>();
        public bool HasUnboundPayments { get; set; }
        public bool HasUnpaidIncomesByCharge { get; set; }
        public bool IsNdflStatusChanged { get; set; }
        
        public List<Ndfl6ReportInitialNdflPaymentDto> NdflPayments { get; set; } =
            new List<Ndfl6ReportInitialNdflPaymentDto>();
    }
}
