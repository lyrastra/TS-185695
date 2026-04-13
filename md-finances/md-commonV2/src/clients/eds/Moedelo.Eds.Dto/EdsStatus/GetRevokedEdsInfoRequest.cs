namespace Moedelo.Eds.Dto.EdsStatus
{
    public sealed class GetRevokedEdsInfoRequest
    {
        public int FirmId { get; }

        public GetRevokedEdsInfoRequest(int firmId)
        {
            FirmId = firmId;
        }
    }
}