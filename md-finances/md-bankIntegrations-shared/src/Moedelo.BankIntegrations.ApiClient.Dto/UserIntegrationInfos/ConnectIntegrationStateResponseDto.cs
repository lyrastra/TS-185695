using Moedelo.BankIntegrations.Enums.UserIntegrationInfos;

namespace Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos
{
    public class ConnectIntegrationStateResponseDto
    {
        public int BankId { get; set; }
        public ConnectIntegrationState State { get; set; }
    }
}