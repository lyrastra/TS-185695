using System;

namespace Moedelo.Dss.Dto
{
    public class ActualCertificateDto
    {
        public string Thumbprint { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsExpired { get; set; }
    }
}