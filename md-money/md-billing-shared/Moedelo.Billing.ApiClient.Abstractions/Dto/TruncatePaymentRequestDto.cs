using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Billing.Abstractions.Dto
{
    public class TruncatePaymentRequestDto
    {
        public int PaymentId { get; set; }
        public DateTime NewEndDate { get; set; }
        public string ChangesAuthorAppName { get; set; }
    }
}
