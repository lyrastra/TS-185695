using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateDetectionResultDto
    {
        public Guid Guid { get; set; }
        public long? SourceId { get; set; }
        public long? SourceBaseId { get; set; }
        public bool IsStrict { get; set; }
    }
}