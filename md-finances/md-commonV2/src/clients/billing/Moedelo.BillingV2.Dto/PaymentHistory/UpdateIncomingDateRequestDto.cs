using System;

namespace Moedelo.BillingV2.Dto.PaymentHistory
{
    public class UpdateIncomingDateRequestDto
    {
        public int PaymentHistoryId { get; set; }
        public DateTime? IncomingDate { get; set; }
    }
}