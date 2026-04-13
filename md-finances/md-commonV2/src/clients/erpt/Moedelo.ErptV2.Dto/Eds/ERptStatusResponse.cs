using Moedelo.Common.Enums.Enums.ElectronicReports;
using System;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class ERptStatusResponse
    {
        public int FirmId { get; set; }
        public string SignatureGuid { get; set; }
        public string AbnGuid { get; set; }
        public string AbnGuidForEmployees { get; set; }
        public long StekUserId { get; set; }
        public DateTime? SignatureExpiryDate { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public SignatureStatus SignatureStatus { get; set; }
        public string SignatureComment { get; set; }
        public PfrStatus PfrStatus { get; set; }
        public virtual DateTime? PfrDocumentUploadTime { get; set; }
        public string PfrComment { get; set; }
        public FnsStatus FnsStatus { get; set; }
        public virtual DateTime? FnsDocumentUploadTime { get; set; }
        public string FnsComment { get; set; }
        public int FnsProvider { get; set; }
        public EdsProvider EdsProvider { get; set; }
        public FsgsStatus RosstatStatus { get; set; }
        public string PfrCommitUser { get; set; }
        public string FnsCommitUser { get; set; }
        public int? PartnerId { get; set; }
        public int? SignatureCommitUserId { get; set; }
        public bool? IsCertificateSigned { get; set; }
        public DateTime? SignatureCreateDate { get; set; }
        public string MemberGuid { get; set; }
        public string PacketGuid { get; set; }
    }
}
