using System;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.Eds.Dto.EdsEventHistory
{
    public sealed class EdsTransferInfoDto
    {
        public string AbnGuid { get; }
        public int? SignatureCommitUserId { get; }
        public string MemberGuid { get; }
        public string PacketGuid { get; }
        public EdsProvider EdsProvider { get; }
        public bool? IsCertificateSigned { get; }
        public DateTime? SignatureCreateDate { get; }
        public DateTime? SignatureExpiry { get; }
        public DateTime? LicenseExpiry { get; }

        public EdsTransferInfoDto(string abnGuid, int? signatureCommitUserId, string memberGuid, string packetGuid, EdsProvider edsProvider, bool? isCertificateSigned,
            DateTime? signatureCreateDate, DateTime? signatureExpiry, DateTime? licenseExpiry)
        {
            AbnGuid = abnGuid;
            SignatureCommitUserId = signatureCommitUserId;
            MemberGuid = memberGuid;
            PacketGuid = packetGuid;
            EdsProvider = edsProvider;
            IsCertificateSigned = isCertificateSigned;
            SignatureCreateDate = signatureCreateDate;
            SignatureExpiry = signatureExpiry;
            LicenseExpiry = licenseExpiry;
        }
    }
}
