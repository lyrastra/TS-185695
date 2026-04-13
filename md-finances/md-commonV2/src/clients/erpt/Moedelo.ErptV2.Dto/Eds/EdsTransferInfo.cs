using System;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class EdsTransferInfo
    {
        public string AbnGuid { get; set; }

        public int? SignatureCommitUserId { get; set; }
        
        public string MemberGuid { get; set; }
        public string PacketGuid { get; set; }
        public int EdsProvider { get; set; }
        public bool? IsCertificateSigned { get; set; }
        public DateTime? SignatureCreateDate { get; set; }
        public DateTime? SignatureExpiry { get; set; }
        public DateTime? LicenseExpiry { get; set; }
    }
}