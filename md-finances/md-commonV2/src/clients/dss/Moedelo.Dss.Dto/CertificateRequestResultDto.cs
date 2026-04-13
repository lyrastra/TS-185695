using System;
using System.Collections.Generic;

namespace Moedelo.Dss.Dto
{
    public class CertificateRequestResultDto
    {
        public string Status { get; set; }
        public List<DssRegistrationErrorDto> Errors { get; set; }
        public string AbnGuid { get; set; }
        public DateTime? CertificateStartDate { get; set; }
        public DateTime? CertificateEndDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public CertificateRevocationInfoDto RevocationInfo { get; set; }
    }
}
