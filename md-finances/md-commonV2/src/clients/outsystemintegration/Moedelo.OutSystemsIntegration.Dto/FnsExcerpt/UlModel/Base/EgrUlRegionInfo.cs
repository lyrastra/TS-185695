using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlRegionInfo
    {
        [XmlAttribute("ТипРегион")]
        public string Type { get; set; }

        [XmlAttribute("НаимРегион")]
        public string Name { get; set; }
    }
}
