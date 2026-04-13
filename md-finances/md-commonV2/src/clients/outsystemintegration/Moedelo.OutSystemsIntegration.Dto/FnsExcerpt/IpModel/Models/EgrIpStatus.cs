using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpStatus
    {
        [XmlAttribute("КодСтатус")]
        public string Code { get; set; }

        [XmlAttribute("НаимСтатус")]
        public string Name { get; set; }
    }
}
