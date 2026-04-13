using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpOrgFSSInfo
    {
        [XmlAttribute("КодФСС")]
        public string Code { get; set; }

        [XmlAttribute("НаимФСС")]
        public string Name { get; set; }
    }
}
