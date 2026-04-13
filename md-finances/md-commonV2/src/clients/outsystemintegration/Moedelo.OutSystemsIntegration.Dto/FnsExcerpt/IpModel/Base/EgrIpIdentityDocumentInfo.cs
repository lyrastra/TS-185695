using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpIdentityDocInfo : EgrIpGrnIpBase
    {
        [XmlAttribute("КодВидДок")]
        public string DocTypeCode { get; set; }

        [XmlAttribute("НаимДок")]
        public string DocName { get; set; }

        [XmlAttribute("СерНомДок")]
        public string SeriesNumber { get; set; }

        [XmlAttribute(AttributeName = "ДатаДок", DataType = "date")]
        public DateTime IssueDate { get; set; }

        [XmlIgnore]
        public bool IssueDateSpecified { get; set; }

        [XmlAttribute("ВыдДок")]
        public string IssueBy { get; set; }

        [XmlAttribute("КодВыдДок")]
        public string DivisionCode { get; set; }
    }
}
