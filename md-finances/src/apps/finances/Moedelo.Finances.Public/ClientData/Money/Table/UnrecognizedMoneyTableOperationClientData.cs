using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Public.ClientData.Money.Table
{
    public class UnrecognizedMoneyTableOperationClientData : MoneyOperationClientData
    {
        public OperationState OperationState { get; set; }
        public MoneyOperationClientData BaseOperation { get; set; }
    }
}