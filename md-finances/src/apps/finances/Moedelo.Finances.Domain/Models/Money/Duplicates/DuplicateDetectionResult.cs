using System;

namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DuplicateDetectionResult
    {
        public Guid Guid { get; set; }

        public long? SourceId { get; set; }

        public long? SourceBaseId { get; set; }

        public bool IsStrict { get; set; }

        public static DuplicateDetectionResult NotFound(Guid guid) =>
            new DuplicateDetectionResult { Guid = guid };
    }
}