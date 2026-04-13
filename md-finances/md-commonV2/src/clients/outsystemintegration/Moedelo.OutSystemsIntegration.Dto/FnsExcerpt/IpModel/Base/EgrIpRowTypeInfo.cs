using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpRowTypeInfo
    {
        [XmlAttribute("КодСПВЗ")]
        public string Code { get; set; }
        
        [XmlAttribute("НаимВидЗап")]
        public string Name { get; set; }
    }
}
