using System;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpCertificateInfoDto
    {
        public EgrIpGrnIpInfoDto InvalidationDate { get; set; }

        public string Series { get; set; }

        public string Number { get; set; }

        public DateTime IssueDate { get; set; }
    }
}