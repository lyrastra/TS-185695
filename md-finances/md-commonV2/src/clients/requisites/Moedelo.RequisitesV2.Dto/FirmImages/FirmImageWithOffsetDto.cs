namespace Moedelo.RequisitesV2.Dto.FirmImages
{
    public class FirmImageWithOffsetDto
    {
        public byte[] Content { get; set; }
        public byte OffsetPct { get; set; } = 0;
    }
}