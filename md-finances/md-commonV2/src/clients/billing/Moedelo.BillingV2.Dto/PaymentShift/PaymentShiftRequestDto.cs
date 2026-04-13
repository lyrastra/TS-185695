using System;

namespace Moedelo.BillingV2.Dto.PaymentShift
{
    public class PaymentShiftRequestDto
    {
        public int PaymentId { get; set; }

        public DateTime NewStartDate { get; set; }
        public string ChangesAuthorAppName { get; set; }
    }
}
