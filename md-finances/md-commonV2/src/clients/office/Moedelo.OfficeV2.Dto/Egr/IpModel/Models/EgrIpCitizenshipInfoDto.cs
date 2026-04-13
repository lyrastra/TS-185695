using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpCitizenshipInfoDto : EgrIpGrnIpBaseDto
    {
        public EgrIpCitizenshipType CitizenshipType { get; set; }

        public string Oksm { get; set; }

        public string Country { get; set; }
    }
}