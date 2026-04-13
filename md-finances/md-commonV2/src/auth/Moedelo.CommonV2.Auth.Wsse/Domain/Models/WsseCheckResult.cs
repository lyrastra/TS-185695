namespace Moedelo.CommonV2.Auth.Wsse.Domain.Models
{
    public class WsseCheckResult
    {
        public ExternalPartnerCredential Partner { get; set; }

        public string Error { get; set; }

        public string InvalidDataError { get; set; }
    }
}