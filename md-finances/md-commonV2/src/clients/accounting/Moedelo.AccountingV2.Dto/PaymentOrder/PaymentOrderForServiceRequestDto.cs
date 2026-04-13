using System;

namespace Moedelo.AccountingV2.Dto.PaymentOrder
{
    public class PaymentOrderForServiceRequestDto
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public int SettlementAccountId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public string PaymentPurpose { get; set; }

        public bool IsWithNds { get; set; }

        public bool IsForOutsource { get; set; }

        public bool NotCreateStatement { get; set; }
    }
}