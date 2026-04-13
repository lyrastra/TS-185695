using Moedelo.Common.Enums.Enums.Tasks;

namespace Moedelo.CommonV2.EventBus.Common.MergeProductResult
{
    public class ProductMergeResultEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public int ProductMergeResultId { get; set; }

        public MergeStatus Status { get; set; }
    }

    public enum MergeStatus
    {
        InProgress = 1,
        Completed = 2,
        Error = 3
    }
}
