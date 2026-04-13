using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpLocalityInfo
    {
        [XmlAttribute("ТипНаселПункт")]
        public string Type { get; set; }

        [XmlAttribute("НаимНаселПункт")]
        public string Name { get; set; }
    }
}
