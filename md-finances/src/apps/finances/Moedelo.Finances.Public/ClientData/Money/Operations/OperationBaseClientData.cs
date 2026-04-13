using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Public.ClientData.Money.Operations
{
    public class OperationBaseClientData
    {
        public long DocumentBaseId { get; set; }
        public MoneyDirection Direction { get; set; }
        public OperationType OperationType { get; set; }
        public OperationKind OperationKind { get; set; }
    }
}