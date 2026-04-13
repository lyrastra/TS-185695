using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentReasonDto
    {
        public long Id { get; set; }

        public string Designation { get; set; }

        public string Description { get; set; }

        public BudgetaryPaymentBase Code { get; set; }
    }
}
