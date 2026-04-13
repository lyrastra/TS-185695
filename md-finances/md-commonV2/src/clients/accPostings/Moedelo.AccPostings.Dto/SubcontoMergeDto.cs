using System;

namespace Moedelo.AccPostings.Dto
{
    public class SubcontoMergeDto
    {
        public long TargetSubcontoId { get; set; }

        public long[] SourceSubcontoIds { get; set; }

        public DateTime StartDate { get; set; }
    }
}