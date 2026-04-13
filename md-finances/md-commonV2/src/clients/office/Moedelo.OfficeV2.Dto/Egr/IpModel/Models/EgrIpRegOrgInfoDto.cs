using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpRegOrgInfoDto
    {
        public EgrIpGrnIpInfoDto GrnIp { get; set; }

        public string TaxAuthorityCode { get; set; }

        public string RegAuthorityAddress { get; set; }
    }
}