using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpAreaInfo
    {
        [XmlAttribute("ТипРайон")]
        public string Type { get; set; }

        [XmlAttribute("НаимРайон")]
        public string Name { get; set; }
    }
}
