using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpNewUlInfo : EgrIpGrnIpBase
    {
        [XmlAttribute("ОГРН")]
        public string Ogrn { get; set; }

        [XmlAttribute("ИНН")]
        public string Inn { get; set; }

        [XmlAttribute("НаимЮЛПолн")]
        public string UlFullName { get; set; }
    }
}
