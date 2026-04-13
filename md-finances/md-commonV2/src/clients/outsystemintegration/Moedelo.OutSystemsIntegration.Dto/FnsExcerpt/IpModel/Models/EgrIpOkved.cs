using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpOkved : EgrIpGrnIpBase
    {
        [XmlAttribute("КодОКВЭД")]
        public string Code { get; set; }

        [XmlAttribute("НаимОКВЭД")]
        public string Name { get; set; }

        [XmlAttribute("ПрВерсОКВЭД")]
        public EgrIpOkvedVersion Version { get; set; }

        [XmlIgnore]
        public bool VersionSpecified { get; set; }
    }
}
