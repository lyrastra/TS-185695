using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models
{
    public class BudgetaryPaymentNdsWidgetRequest
    {
        public int Year { get; set; }
        public int? Quarter { get; set; }
        public int KbkId { get; set; }

        public BudgetaryAccountCodes BudgetaryAccountCode { get; set; }
    }
}