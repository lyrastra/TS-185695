using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpRegionInfo
    {
        [XmlAttribute("ТипРегион")]
        public string Type { get; set; }

        [XmlAttribute("НаимРегион")]
        public string Name { get; set; }
    }
}
