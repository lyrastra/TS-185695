using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlLocalityInfo
    {
        [XmlAttribute("ТипНаселПункт")]
        public string Type { get; set; }

        [XmlAttribute("НаимНаселПункт")]
        public string Name { get; set; }
    }
}
