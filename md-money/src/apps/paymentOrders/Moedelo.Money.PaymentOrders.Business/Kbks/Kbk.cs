using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Business.Kbks
{
    public class Kbk
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public KbkPaymentType KbkPaymentType { get; set; }
        public BudgetaryPaymentBase PaymentBase { get; set; }
        public string DocNumber { get; set; }
        public BudgetaryAccountCodes AccountCode { get; set; }
    }
}
