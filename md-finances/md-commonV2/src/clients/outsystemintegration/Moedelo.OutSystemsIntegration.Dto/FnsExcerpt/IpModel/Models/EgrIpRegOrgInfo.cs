using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpRegOrgInfo
    {
        [XmlElement("ГРНИПДата")]
        public EgrIpGrnIpInfo GrnIp { get; set; }

        [XmlAttribute("КодНО")]
        public string TaxAuthorityCode { get; set; }

        [XmlAttribute("АдрРО")]
        public string RegAuthorityAddress { get; set; }
    }
}
