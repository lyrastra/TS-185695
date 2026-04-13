using System;
using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpBirthInfoDto : EgrIpGrnIpBaseDto
    {
        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public EgrIpSignCompletenessBirthDate SignCompletenessBirthDate { get; set; }

        public bool SignCompletenessBirthDateSpecified { get; set; }
    }
}