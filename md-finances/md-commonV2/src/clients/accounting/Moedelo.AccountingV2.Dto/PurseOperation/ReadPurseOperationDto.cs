using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.AccountingV2.Dto.PurseOperation
{
    public class ReadPurseOperationDto
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public OperationType OperationType { get; set; }
    }
}
