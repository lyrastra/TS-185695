using System;

namespace Moedelo.AccountV2.Dto.ExternalApi
{
    public class BillingBlTarifChangedDto
    {
        public int PaymentId { get; set; }

        public int FirmId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}