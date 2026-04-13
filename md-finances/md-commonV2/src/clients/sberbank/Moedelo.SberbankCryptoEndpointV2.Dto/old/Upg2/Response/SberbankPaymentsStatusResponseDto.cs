using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response
{
    public class SberbankPaymentsStatusResponseDto
    {
        public Guid SberbankPaymentGuid { get; set; }
        public SberbankPaymentStatus SberbankPaymentStatus { get; set; }
    }
}