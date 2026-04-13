using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.MissingEmployee
{
    public class MissingEmployeePaymentOrdersResponse
    {
        public long PaymentBaseId { get; set; }
        public OperationType OperationType { get; set; }
    }
}