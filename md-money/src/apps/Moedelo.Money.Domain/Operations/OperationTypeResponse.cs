using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.Operations
{
    public class OperationTypeResponse
    {
        public long DocumentBaseId { get; set; }
        public OperationType OperationType { get; set; }
    }
}
