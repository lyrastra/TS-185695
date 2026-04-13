using System;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class DuplicateDetectionResponseDto
    {
        public Guid Guid { get; set; }
        public long? SourceId { get; set; }
        public long? SourceBaseId { get; set; }
        public bool IsStrict { get; set; }
    }
}