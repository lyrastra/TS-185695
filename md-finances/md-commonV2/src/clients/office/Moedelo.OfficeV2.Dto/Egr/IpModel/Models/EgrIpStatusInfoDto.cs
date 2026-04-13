using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpStatusInfoDto
    {
        public EgrIpStatusDto Status { get; set; }

        public EgrIpGrnIpInfoDto GrnIp { get; set; }
    }
}
