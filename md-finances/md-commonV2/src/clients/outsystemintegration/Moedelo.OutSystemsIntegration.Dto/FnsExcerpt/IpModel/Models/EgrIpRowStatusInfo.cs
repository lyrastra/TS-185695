using System.Collections.Generic;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpRowStatusInfo
    {
        [XmlElement("ГРНИПДатаНед")]
        public EgrIpRowGrnIpInfo GrnIpInvalidated { get; set; }

        [XmlElement("ГРНИПДатаИспр")]
        public List<EgrIpRowGrnIpInfo> GrnIpCorrection { get; set; }
    }
}
