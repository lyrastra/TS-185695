using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class OperationKindAndBaseId
    {
        public OperationKind OperationKind { get; set; }
        public long DocumentBaseId { get; set; }
    }
}