using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpCertificateInfo
    {
        [XmlElement("ГРНИПДатаСвидНед")]
        public EgrIpGrnIpInfo InvalidationDate { get; set; }

        [XmlAttribute("Серия")]
        public string Series { get; set; }

        [XmlAttribute("Номер")]
        public string Number { get; set; }

        [XmlAttribute("ДатаВыдСвид", DataType = "date")]
        public DateTime IssueDate { get; set; }
    }
}
