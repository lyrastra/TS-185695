using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequestsPart
{
    public class SetIntegrationRequestPartStatusWithVerificationRequestDto
    {
        public IntegrationRequestPartStatusEnum Status { get; set; }

        public IntegrationRequestPartStatusEnum OldStatus { get; set; }
    }
}
