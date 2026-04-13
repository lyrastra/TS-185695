using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment
{
    public class BudgetaryPaymentTaxWidgetRequest
    {
        public int Year { get; set; }
        public int? Quarter { get; set; }
        public int KbkId { get; set; }
        public BudgetaryAccountCodes BudgetaryAccountCode { get; set; }
    }
}