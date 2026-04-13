namespace Moedelo.ErptV2.Dto.Eds
{
    public class DocumentStepEdsTransferInfo
    {
        public DocumentStepEdsTransferInfo()
        {
        }

        public DocumentStepEdsTransferInfo(bool isTransfer, bool isSendingViaEdmAvailable, int kontragentId)
        {
            IsTransfer = isTransfer;
            IsSendingViaEdmAvailable = isSendingViaEdmAvailable;
            KontragentId = kontragentId;
        }

        public bool IsTransfer { get; set; }
        public bool IsSendingViaEdmAvailable { get; set; }
        public int KontragentId { get; set; }
    }
}