using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpLicenseInfo : EgrIpGrnIpBase
    {
        [XmlElement("НаимЛицВидДеят")]
        public List<string> TypeName { get; set; }

        [XmlElement("МестоДейстЛиц")]
        public List<string> PlaceActivity { get; set; }

        [XmlElement("ЛицОргВыдЛиц")]
        public string Licensor { get; set; }

        [XmlElement("СвПриостЛиц")]
        public EgrIpLicenseSuspensionInfo LicenseSuspension { get; set; }

        [XmlAttribute("СерЛиц")]
        public string Series { get; set; }

        [XmlAttribute("НомЛиц")]
        public string Number { get; set; }

        [XmlAttribute("ВидЛиц")]
        public string Type { get; set; }

        [XmlAttribute("ДатаЛиц", DataType = "date")]
        public DateTime Date { get; set; }

        [XmlAttribute("ДатаНачЛиц", DataType = "date")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("ДатаОкончЛиц", DataType = "date")]
        public DateTime EndDate { get; set; }

        [XmlIgnore]
        public bool EndDateSpecified { get; set; }
    }
}
