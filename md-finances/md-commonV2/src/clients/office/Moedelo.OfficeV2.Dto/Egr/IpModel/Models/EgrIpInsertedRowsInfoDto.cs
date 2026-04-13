using System;
using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpInsertedRowsInfoDto
    {
        public EgrIpRowTypeInfoDto Type { get; set; }

        public EgrIpRowRegOrgInfoDto RegOrgInfo { get; set; }

        public List<EgrIpDocInfoDto> PresentDocs { get; set; }

        public List<EgrIpCertificateInfoDto> Certificates { get; set; }

        public EgrIpRowGrnIpInfoDto GrnIpCorrection { get; set; }

        public EgrIpRowGrnIpInfoDto GrnIpInvalidated { get; set; }

        public EgrIpRowStatusInfoDto Status { get; set; }

        public string RowId { get; set; }

        public string GrnIp { get; set; }

        public DateTime Date { get; set; }
    }
}
