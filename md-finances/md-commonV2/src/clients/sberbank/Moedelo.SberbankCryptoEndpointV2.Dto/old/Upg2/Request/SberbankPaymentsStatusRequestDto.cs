using System;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Request
{
    public class SberbankPaymentsStatusRequestDto
    {
        public Guid SberbankPaymentGuid { get; set; }
        public int FirmId { get; set; }
    }
}