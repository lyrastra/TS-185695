using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class UnrecognizedMoneyTableOperation : MoneyTableOperation
    {
        public MoneyOperation BaseOperation { get; set; }
    }
}