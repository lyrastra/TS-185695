using System;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpTaxRegInfoDto : EgrIpGrnIpBaseDto
    {
        public EgrIpTaxAuthorityInfoDto TaxAuthority { get; set; }

        public string InnFl { get; set; }

        public DateTime RegDate { get; set; }
    }
}