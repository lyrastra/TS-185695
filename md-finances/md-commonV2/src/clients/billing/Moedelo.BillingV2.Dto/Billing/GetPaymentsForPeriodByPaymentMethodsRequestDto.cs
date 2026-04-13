using System;
using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class GetPaymentsForPeriodByPaymentMethodsRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<string> PaymentMethods { get; set; }
    }
}