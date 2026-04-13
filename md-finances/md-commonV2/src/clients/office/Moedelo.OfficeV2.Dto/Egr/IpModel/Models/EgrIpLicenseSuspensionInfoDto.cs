using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpLicenseSuspensionInfoDto : EgrUlGrnDateBaseInfoDto
    {
        public DateTime Date { get; set; }

        public string Licensor { get; set; }
    }
}
