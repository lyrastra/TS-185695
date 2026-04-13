using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpRegInfo
    {
        [XmlElement("СвКФХ")]
        public EgrIpPeasantFarmInfo PeasantFarmInfo { get; set; }

        [XmlAttribute("ОГРНИП")]
        public string OgrnIp { get; set; }

        [XmlAttribute("ДатаОГРНИП", DataType = "date")]
        public DateTime OgrnIpDate { get; set; }

        [XmlAttribute("РегНом")]
        public string RegNumber { get; set; }

        [XmlAttribute("ДатаРег", DataType = "date")]
        public DateTime RegDate { get; set; }

        [XmlIgnore]
        public bool RegDateSpecified { get; set; }

        [XmlAttribute("НаимРО")]
        public string RegAuthorityName { get; set; }
    }
}
