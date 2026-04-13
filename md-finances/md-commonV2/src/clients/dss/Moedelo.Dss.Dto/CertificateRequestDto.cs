namespace Moedelo.Dss.Dto
{
    public class CertificateRequestDto
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public bool IsTestMode { get; set; }
    }
}
