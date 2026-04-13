using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments
{
    public class WorkerChargePaymentsDto
    {
        public int WorkerId { get; set; }

        public IReadOnlyCollection<ChargePaymentDto> ChargePayments { get; set; }
    }
}
