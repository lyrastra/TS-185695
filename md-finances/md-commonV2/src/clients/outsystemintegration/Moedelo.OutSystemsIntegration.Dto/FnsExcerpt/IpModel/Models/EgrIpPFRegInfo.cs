using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpPFRegInfo : EgrIpGrnIpBase
    {
        [XmlElement("СвОргПФ")]
        public EgrIpOrgPFInfo Org { get; set; }

        [XmlAttribute("РегНомПФ")]
        public string Number { get; set; }

        [XmlAttribute("ДатаРег", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
