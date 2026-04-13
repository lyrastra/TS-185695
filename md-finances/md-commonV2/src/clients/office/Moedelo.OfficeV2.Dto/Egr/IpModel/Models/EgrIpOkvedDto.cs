using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpOkvedDto : EgrIpGrnIpBaseDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public EgrIpOkvedVersion Version { get; set; }

        public bool VersionSpecified { get; set; }
    }
}