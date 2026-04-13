using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SberbankPaymentRequest
{
    public class GetAllowedAdvanceAcceptancesByFirmIdListDto
    {
        public IReadOnlyCollection<int> FirmIdList { get; set; }
    }
}
