using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequestsPart
{
    public class SetIntegrationRequestPartStatusRequestDto
    {
        public IntegrationRequestPartStatusEnum Status { get; set; }

        public string LogFile { get; set; }
    }
}
