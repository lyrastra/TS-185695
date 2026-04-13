using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpNewUlInfoDto : EgrIpGrnIpBaseDto
    {
        public string Ogrn { get; set; }

        public string Inn { get; set; }

        public string UlFullName { get; set; }
    }
}
