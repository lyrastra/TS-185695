namespace Moedelo.Eds.Dto.EdsStatus
{
    public sealed class SaveEdsCommentRequest
    {
        public int FirmId { get; }
        public string Comment { get; }

        public SaveEdsCommentRequest(int firmId, string comment)
        {
            FirmId = firmId;
            Comment = comment;
        }
    }
}