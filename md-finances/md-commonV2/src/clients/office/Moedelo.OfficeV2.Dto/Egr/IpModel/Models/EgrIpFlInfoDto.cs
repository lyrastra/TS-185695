using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpFlInfoDto : EgrIpGrnIpBaseDto
    {
        public EgrIpFioInfoDto FioRus { get; set; }

        public EgrIpFioInfoDto FioLat { get; set; }

        public EgrIpGender Gender { get; set; }
    }
}
