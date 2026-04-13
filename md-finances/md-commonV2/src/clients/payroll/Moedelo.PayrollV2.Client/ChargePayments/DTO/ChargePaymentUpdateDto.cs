using System;

namespace Moedelo.PayrollV2.Client.ChargePayments.DTO
{
    public class ChargePaymentUpdateDto
    {
        public long DocumentBaseId { get; set; }

        public bool IsPaid { get; set; }

        public DateTime ChargePaymentDate { get; set; }
    }
}
