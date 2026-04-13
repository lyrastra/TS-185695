using System.Collections.Generic;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.PaymentRequest
{
    public class GetAllAdvanceAcceptancesResponseDto
    {
        public List<AdvanceAcceptanceDto> Subscribers { get; set; } = new List<AdvanceAcceptanceDto>();
    }
}