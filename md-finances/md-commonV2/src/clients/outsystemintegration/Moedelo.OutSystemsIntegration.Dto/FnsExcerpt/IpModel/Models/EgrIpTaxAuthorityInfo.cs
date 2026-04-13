using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpTaxAuthorityInfo
    {
        [XmlAttribute("КодНО")]
        public string Code { get; set; }

        [XmlAttribute("НаимНО")]
        public string Name { get; set; }
    }
}
