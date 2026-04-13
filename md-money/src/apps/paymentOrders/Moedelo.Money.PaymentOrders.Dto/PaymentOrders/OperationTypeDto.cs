using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders
{
    public class OperationTypeDto
    {
        public long DocumentBaseId { get; set; }
        public OperationType OperationType { get; set; }
    }
}
