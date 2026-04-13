using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentTaxWidgetRequest
    {
        public int Year { get; set; }
        public int? Quarter { get; set; }
        public int KbkId { get; set; }
        
        public BudgetaryAccountCodes BudgetaryAccountCode { get; set; }
    }
}