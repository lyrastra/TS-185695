namespace Moedelo.BackofficeV2.Dto.Partner
{
    public class PartnerEmployeeStatisticResponse
    {
        public PartnerEmployeeStatisticResponse() { }

        public PartnerEmployeeStatisticResponse(byte[] fileContent)
        {
            FileContent = fileContent;
        }

        public byte[] FileContent { get; set; }
    }
}
