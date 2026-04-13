using System;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class CertificateSignedInfo
    {
        public bool IsCertificateSigned { get; set; }
        public bool NeedToSignCertificate { get; set; }
        public DateTime? SigningLastDate { get; set; }
        public bool EReportsIsAvailable { get; set; }
    }
}
