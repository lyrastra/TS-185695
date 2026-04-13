using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class SetMultipleIntegrationRequestPartsStatusRequestDto
    {
        public IReadOnlyCollection<int> IntegrationRequestIds { get; set; }
        public IntegrationRequestPartStatusEnum NewStatus { get; set; }
    }
}
