using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpTerminationStatus
    {
        [XmlAttribute("КодСтатус")]
        public string Code { get; set; }

        [XmlAttribute("НаимСтатус")]
        public string Name { get; set; }

        [XmlAttribute("ДатаПрекращ", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
