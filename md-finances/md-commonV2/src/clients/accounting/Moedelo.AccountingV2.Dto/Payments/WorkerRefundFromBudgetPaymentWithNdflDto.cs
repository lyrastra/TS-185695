using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class WorkerRefundFromBudgetPaymentWithNdflDto
    {
        public WorkerRefundFromBudgetPaymentWithNdflDto()
        {
            NdflPayments = new List<NdflRefundFromBudgetPaymentForReportDto>();
        }

        public List<NdflRefundFromBudgetPaymentForReportDto> NdflPayments { get; set; } 
    }
}