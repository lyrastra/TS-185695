using System;
using Moedelo.OfficeV2.Dto.Egr.IpModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Models
{
    public class EgrIpPFRegInfoDto : EgrIpGrnIpBaseDto
    {
        public EgrIpOrgPFInfoDto Org { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }
    }
}