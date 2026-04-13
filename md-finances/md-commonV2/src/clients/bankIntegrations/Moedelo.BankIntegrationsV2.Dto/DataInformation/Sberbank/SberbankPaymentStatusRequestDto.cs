using System;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank
{
    public class SberbankPaymentStatusRequestDto
    {
        public Guid SberbankPaymentGuid { get; set; }
        public int FirmId { get; set; }
    }
}