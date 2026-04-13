using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpDocInfo
    {
        [XmlElement("НаимДок")]
        public string Name { get; set; }

        [XmlElement("НомДок")]
        public string Number { get; set; }

        [XmlElement("ДатаДок", DataType = "date", IsNullable = true)]
        public DateTime? Date { get; set; }
    }
}
