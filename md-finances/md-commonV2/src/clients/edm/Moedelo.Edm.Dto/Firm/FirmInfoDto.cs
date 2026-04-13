namespace Moedelo.Edm.Dto.Firm
{
    public class FirmInfoDto
    {
        public int FirmId { get; set; }

        public bool EdmEnabled { get; set; }

        public int EdmKontragentCount { get; set; }

        public int DocumentCountSendedByYear { get; set; }

        public int DocumentCountReceivedByYear { get; set; }
    }
}
