using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpOrgPFInfo
    {
        [XmlAttribute("КодПФ")]
        public string Code { get; set; }

        [XmlAttribute("НаимПФ")]
        public string Name { get; set; }
    }
}
