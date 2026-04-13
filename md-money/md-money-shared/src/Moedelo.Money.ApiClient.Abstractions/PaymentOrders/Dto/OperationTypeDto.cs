using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class OperationTypeDto
    {
        public long DocumentBaseId { get; set; }
        public OperationType OperationType { get; set; }
    }
}