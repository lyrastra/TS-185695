using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments
{
    public class ChargePaymentUpdateDto
    {
        public long DocumentBaseId { get; set; }

        public bool IsPaid { get; set; }

        public DateTime ChargePaymentDate { get; set; }
    }
}
