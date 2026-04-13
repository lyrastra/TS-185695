using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpOkvedInfoDto
    {
        public EgrIpOkvedDto Main { get; set; }

        public List<EgrIpOkvedDto> Other { get; set; }
    }
}