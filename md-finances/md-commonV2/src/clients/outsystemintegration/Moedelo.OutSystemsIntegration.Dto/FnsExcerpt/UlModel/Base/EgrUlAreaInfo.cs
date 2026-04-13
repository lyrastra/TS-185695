using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlAreaInfo
    {
        [XmlAttribute("ТипРайон")]
        public string Type { get; set; }

        [XmlAttribute("НаимРайон")]
        public string Name { get; set; }
    }
}
