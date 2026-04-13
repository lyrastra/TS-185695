using System;

namespace Moedelo.Dss.Dto
{
    public sealed class CertificateInfoDto
    {
        public string Inn { get; set; }
        public string Issuer { get; set; }
        public string Thumbprint { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActual { get; set; }
    }
}