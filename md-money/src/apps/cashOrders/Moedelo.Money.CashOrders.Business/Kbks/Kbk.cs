using Moedelo.Money.Enums;

namespace Moedelo.Money.CashOrders.Business.Kbks
{
    public class Kbk
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public BudgetaryAccountCodes AccountCode { get; set; }
    }
}
