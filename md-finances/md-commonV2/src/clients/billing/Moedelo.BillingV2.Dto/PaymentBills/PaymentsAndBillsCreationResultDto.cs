using System;

namespace Moedelo.BillingV2.Dto.PaymentBills
{
    public class PaymentsAndBillsCreationResultDto
    {
        public Guid RequestGuid { get; set; }
        public int PaymentHistoryId { get; set; }
    }
}