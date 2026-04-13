using System;
using System.Collections.Generic;

namespace Moedelo.Dss.Dto
{
    public class CertificateRevocationInfoDto
    {
        public DateTime Date { get; set; }
        public string SerialNumber { get; set; }
        public string Thumbprint { get; set; }
        public IReadOnlyList<string> PacketGuids { get; set; }
    }
}
