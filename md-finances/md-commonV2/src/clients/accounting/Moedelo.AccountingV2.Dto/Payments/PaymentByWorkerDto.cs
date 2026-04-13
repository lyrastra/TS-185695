using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class PaymentByWorkerDto
    {
        public PaymentByWorkerDto()
        {
            Payments = new List<SavedWorkerPaymentDto>();
        }

        public List<SavedWorkerPaymentDto> Payments { get; set; }
    }
}