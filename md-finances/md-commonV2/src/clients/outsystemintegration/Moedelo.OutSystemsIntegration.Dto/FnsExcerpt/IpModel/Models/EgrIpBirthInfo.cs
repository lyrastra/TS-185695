using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpBirthInfo : EgrIpGrnIpBase
    {
        [XmlAttribute("ДатаРожд", DataType = "date")]
        public DateTime BirthDate { get; set; }

        [XmlAttribute("МестоРожд")]
        public string BirthPlace { get; set; }

        [XmlAttribute("ПрДатаРожд")]
        public EgrIpSignCompletenessBirthDate SignCompletenessBirthDate { get; set; }

        [XmlIgnore]
        public bool SignCompletenessBirthDateSpecified { get; set; }
    }
}
