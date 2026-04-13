using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlStreetInfo
    {
        [XmlAttribute("ТипУлица")]
        public string Type { get; set; }

        [XmlAttribute("НаимУлица")]
        public string Name { get; set; }
    }
}
