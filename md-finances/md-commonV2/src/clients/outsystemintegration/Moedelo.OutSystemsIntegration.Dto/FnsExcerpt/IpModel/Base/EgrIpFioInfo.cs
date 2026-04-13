using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpFioInfo
    {
        [XmlAttribute("Фамилия")]
        public string Surname { get; set; }
        
        [XmlAttribute("Имя")]
        public string Name { get; set; }
        
        [XmlAttribute("Отчество")]
        public string Patronymic { get; set; }
    }
}
