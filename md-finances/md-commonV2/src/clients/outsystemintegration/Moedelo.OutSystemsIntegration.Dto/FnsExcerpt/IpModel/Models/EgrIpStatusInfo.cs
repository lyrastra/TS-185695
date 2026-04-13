using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpStatusInfo
    {
        [XmlElement("СвСтатус")]
        public EgrIpStatus Status { get; set; }

        [XmlElement("ГРНИПДата")]
        public EgrIpGrnIpInfo GrnIp { get; set; }
    }
}
