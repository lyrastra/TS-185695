using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpCitizenshipInfo : EgrIpGrnIpBase
    {
        [XmlAttribute("ВидГражд")]
        public EgrIpCitizenshipType CitizenshipType { get; set; }

        [XmlAttribute("ОКСМ")]
        public string Oksm { get; set; }

        [XmlAttribute("НаимСтран")]
        public string Country { get; set; }
    }
}
