using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;

public class ConnectIntegrationStateRequestDto
{
    public int FirmId { get; set; }
    public List<int> BankIds { get; set; }
}