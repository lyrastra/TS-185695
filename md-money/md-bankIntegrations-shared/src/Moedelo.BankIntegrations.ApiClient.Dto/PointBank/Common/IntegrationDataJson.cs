using System.Collections.Generic;
using Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Consents;

namespace Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Common;

public class IntegrationDataJson
{
    public List<ConsentDto> Consents { get; set; }

    public List<ConsentDto> ChildConsents { get; set; }
}