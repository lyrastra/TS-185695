using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpFSSRegInfo : EgrIpGrnIpBase
    {
        [XmlElement("СвОргФСС")]
        public EgrIpOrgFSSInfo Org { get; set; }

        [XmlAttribute("РегНомФСС")]
        public string Number { get; set; }

        [XmlAttribute("ДатаРег", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
