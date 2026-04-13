using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.MissingEmployee
{
    public class MissingEmployeePayment
    {
        public long DocumentBaseId { get; set; }
        public OperationType OperationType { get; set; }
    }
}