using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank
{
    public class GetSberbankPaymentStatusResponseDto
    {
        public List<SberbankPaymentStatusResponseDto> List { get; set; }
    }
}