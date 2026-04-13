namespace Moedelo.PayrollV2.Client.MdPulse.DTO
{
    public class FirmMrotStatusDto
    {
        public int FirmId { get; set; }
        public string RegionCode { get; set; }
        public MrotStatus MrotStatus { get; set; }
    }
}