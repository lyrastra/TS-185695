using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class GetAllowedAdvanceAcceptancesByFirmIdListDto
    {
        public IReadOnlyCollection<int> FirmIdList { get; set; }
    }
}
