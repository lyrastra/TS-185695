using System.Collections.Generic;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response
{
    public class GetSberbankSettlementsToSsoResponseDto : ResponseSberbankCryptoDto
    {
        public List<SettlementAccountDto> Settlements { get; set; }
    }
}