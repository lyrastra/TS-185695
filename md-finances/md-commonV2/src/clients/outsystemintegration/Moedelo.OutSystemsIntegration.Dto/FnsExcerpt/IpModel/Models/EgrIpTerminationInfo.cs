using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpTerminationInfo
    {
        [XmlElement("СвСтатус")]
        public EgrIpTerminationStatus Status { get; set; }

        [XmlElement("ГРНИПДата")]
        public EgrIpGrnIpInfo GrnIp { get; set; }

        [XmlElement("СвНовЮЛ")]
        public EgrIpNewUlInfo NewUlInfo { get; set; }
    }
}
