using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class SalaryPaymentsDto
    {
        public List<WorkerPaymentDto> Workers { get; set; }

        public NdflPaymentDto Ndfl { get; set; }
    }
}