using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.PurseOperations.Dtos
{
    public class ReadPurseOperationDto
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public OperationType OperationType { get; set; }
    }
}
