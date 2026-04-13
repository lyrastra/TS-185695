using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpTerminationInfoDto
    {
        public EgrIpTerminationStatusDto Status { get; set; }

        public EgrIpGrnIpInfoDto GrnIp { get; set; }

        public EgrIpNewUlInfoDto NewUlInfo { get; set; }
    }
}