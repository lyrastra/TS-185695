using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class WorkerPaymentWithNdflDto
    {
        public WorkerPaymentWithNdflDto()
        {
            Payments = new List<SavedWorkerPaymentDto>();
            NdflPayments = new List<NdflPaymentForReportDto>();
        }

        public List<SavedWorkerPaymentDto> Payments { get; set; }

        public List<NdflPaymentForReportDto> NdflPayments { get; set; } 
    }
}