using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank
{
    public class GetSberbankPaymentStatusRequestDto
    {
        public List<SberbankPaymentStatusRequestDto> SberbankPayments { get; set; }
    }
}