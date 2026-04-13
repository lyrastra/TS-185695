using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;

public class IntegrationRequestHistoryUnitDto
{
    public string Timestamp { get; set; }
    public string XmlRequest { get; set; }
    public string XmlResponse { get; set; }
}
