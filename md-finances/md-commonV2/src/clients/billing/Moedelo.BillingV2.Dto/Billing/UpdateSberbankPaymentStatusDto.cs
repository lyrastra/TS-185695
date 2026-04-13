using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class UpdateSberbankPaymentStatusDto
    {
        public int Id { get; set; }
        public SberbankPaymentStatus Status { get; set; }
    }
}
