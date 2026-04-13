using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.Billing.PaymentPositions
{
    public class UpdatePaymentPositionsDto
    {
        public int PaymentId { get; set; }

        public List<PaymentPositionDto> Positions { get; set; }
    }
}
