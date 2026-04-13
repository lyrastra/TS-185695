using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpStreetInfo
    {
        [XmlAttribute("ТипУлица")]
        public string Type { get; set; }

        [XmlAttribute("НаимУлица")]
        public string Name { get; set; }
    }
}
