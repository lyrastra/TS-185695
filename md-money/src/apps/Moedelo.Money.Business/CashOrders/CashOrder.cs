using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.CashOrders
{
    internal sealed class CashOrder
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public OperationType OperationType { get; set; }

        public MoneyDirection Direction { get; set; }
    }
}
