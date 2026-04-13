using Moedelo.Money.Enums;

namespace Moedelo.Money.Common.Domain.Models
{
    public class OperationTypeResponse
    {
        public long DocumentBaseId { get; set; }

        public OperationType OperationType { get; set; }
    }
}
