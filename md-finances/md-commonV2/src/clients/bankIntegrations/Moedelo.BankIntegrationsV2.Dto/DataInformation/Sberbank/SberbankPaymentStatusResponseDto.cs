using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank
{
    public class SberbankPaymentStatusResponseDto
    {
        public int FirmId { get; set; }
        public SberbankPaymentStatus SberbankPaymentStatus { get; set; }
        public Guid SberbankPaymentGuid { get; set; }
        public SberbankPaymentRequestStatus SberbankPaymentRequestStatus { get; set; }
        public string BankStatus { get; set; }
    }
}