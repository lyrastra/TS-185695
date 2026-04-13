using System;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpRFResidenceRightInfoDto : EgrIpGrnIpBaseDto
    {
        public EgrIpIdentityDocInfoDto RFResidenceRightDoc { get; set; }

        public DateTime ActionPeriod { get; set; }
    }
}