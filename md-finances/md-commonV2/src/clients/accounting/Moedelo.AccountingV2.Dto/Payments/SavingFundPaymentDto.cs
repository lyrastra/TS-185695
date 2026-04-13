using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class SavingFundPaymentDto
    {
        public SavingFundPaymentDto()
        {
            Payments = new List<PaymentOrderDto>();
        }

        public List<PaymentOrderDto> Payments { get; set; }

        public int? SettlementAccountId { get; set; }
    }
}