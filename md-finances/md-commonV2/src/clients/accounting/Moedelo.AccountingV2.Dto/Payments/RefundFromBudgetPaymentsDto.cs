using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class RefundFromBudgetPaymentsDto
    {
        public RefundFromBudgetPaymentsDto()
        {
            Payments = new List<RefundFromBudgetPaymentDto>();
        }

        public List<RefundFromBudgetPaymentDto> Payments { get; set; }
    }
}
