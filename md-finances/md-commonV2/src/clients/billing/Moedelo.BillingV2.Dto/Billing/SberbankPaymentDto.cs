using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class SberbankPaymentDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public Guid SberbankPaymentGuid { get; set; }

        public SberbankPaymentStatus SberbankPaymentStatus { get; set; }
    }
}