using Moedelo.BankIntegrationsV2.Dto.Integrations;

namespace Moedelo.BankIntegrationsV2.Dto.ExternalPartner.RobokassaAcquirer
{
    public class RobokassaGetLinkToPayRequestDto
    {
        public PaymentOrderDto PaymentOrder { get; set; }

        public int FirmId { get; set; }
    }
}
