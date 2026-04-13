using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpFlInfo : EgrIpGrnIpBase
    {
        [XmlElement("ФИОРус")]
        public EgrIpFioInfo FioRus { get; set; }

        [XmlElement("ФИОЛат")]
        public EgrIpFioInfo FioLat { get; set; }

        [XmlAttribute("Пол")]
        public EgrIpGender Gender { get; set; }
    }
}
