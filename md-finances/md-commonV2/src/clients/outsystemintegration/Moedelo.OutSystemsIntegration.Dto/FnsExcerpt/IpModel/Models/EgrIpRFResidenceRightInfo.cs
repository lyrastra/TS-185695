using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpRFResidenceRightInfo : EgrIpGrnIpBase
    {
        [XmlElement("ДокПравЖитРФ")]
        public EgrIpIdentityDocInfo RFResidenceRightDoc { get; set; }

        [XmlAttribute("СрокДействДок", DataType = "date")]
        public DateTime ActionPeriod { get; set; }
    }
}
