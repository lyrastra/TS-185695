namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Request
{
    public class StatusSrpDto
    {
        public StatusSrpDto(string[] requestIds)
        {
            RequestIds = requestIds;
        }

        public string[] RequestIds { get; set; }
    }
}