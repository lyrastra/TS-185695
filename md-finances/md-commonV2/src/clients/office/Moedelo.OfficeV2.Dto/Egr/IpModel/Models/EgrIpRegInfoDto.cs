using System;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpRegInfoDto
    {
        public EgrIpPeasantFarmInfoDto PeasantFarmInfo { get; set; }

        public string OgrnIp { get; set; }

        public DateTime OgrnIpDate { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public bool RegDateSpecified { get; set; }

        public string RegAuthorityName { get; set; }
    }
}