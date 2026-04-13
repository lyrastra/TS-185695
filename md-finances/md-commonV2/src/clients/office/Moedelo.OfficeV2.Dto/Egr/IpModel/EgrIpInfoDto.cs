using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Models;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel
{
    public class EgrIpInfoDto
    {
        public string InnFl { get; set; }

        public string OgrnIp { get; set; }

        public DateTime ExtractDate { get; set; }

        public DateTime OgrnDate { get; set; }

        public EgrIpType IpType { get; set; }

        public string IpTypeName { get; set; }

        public EgrIpFlInfoDto FlInfo { get; set; }

        public EgrIpBirthInfoDto BirthInfo { get; set; }

        public EgrIpCitizenshipInfoDto Citizenship { get; set; }

        public EgrIpFlIdentityDocInfoDto IdentityDoc { get; set; }

        public EgrIpRFResidenceRightInfoDto RFResidenceRight { get; set; }

        public EgrIpHomeAddressInfoDto HomeAddress { get; set; }

        public EgrIpEmailInfoDto EmailInfo { get; set; }

        public EgrIpRegInfoDto IpRegInfo { get; set; }

        public EgrIpRegOrgInfoDto RegOrgInfo { get; set; }

        public EgrIpStatusInfoDto StatusInfo { get; set; }

        public EgrIpTerminationInfoDto TerminationInfo { get; set; }

        public EgrIpTaxRegInfoDto TaxRegInfo { get; set; }

        public EgrIpPFRegInfoDto PFRegInfo { get; set; }

        public EgrIpFSSRegInfoDto FSSRegInfo { get; set; }

        public EgrIpOkvedInfoDto OkvedInfo { get; set; }

        public List<EgrIpLicenseInfoDto> License { get; set; }

        public List<EgrIpInsertedRowsInfoDto> InsertedRowsInfo { get; set; }
    }
}