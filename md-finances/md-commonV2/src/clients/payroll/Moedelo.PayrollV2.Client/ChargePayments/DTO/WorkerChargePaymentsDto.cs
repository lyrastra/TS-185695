using System.Collections.Generic;

namespace Moedelo.PayrollV2.Client.ChargePayments.DTO
{
    public class WorkerChargePaymentsDto
    {
        public int WorkerId { get; set; }
        public IList<ChargePaymentDto> ChargePayments { get; set; }
    }
}
