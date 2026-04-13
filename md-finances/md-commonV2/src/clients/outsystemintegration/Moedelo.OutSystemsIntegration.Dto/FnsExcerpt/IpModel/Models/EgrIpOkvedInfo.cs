using System.Collections.Generic;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpOkvedInfo
    {
        [XmlElement("СвОКВЭДОсн")]
        public EgrIpOkved Main { get; set; }

        [XmlElement("СвОКВЭДДоп")]
        public List<EgrIpOkved> Other { get; set; }
    }
}
