using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpInsertedRowsInfo
    {
        [XmlElement("ВидЗап")]
        public EgrIpRowTypeInfo Type { get; set; }

        [XmlElement("НаимСтатус")]
        public EgrIpRegOrgInfo RegOrgInfo { get; set; }

        [XmlElement("СведПредДок")]
        public List<EgrIpDocInfo> PresentDocs { get; set; }

        [XmlElement("СвСвид")]
        public List<EgrIpCertificateInfo> Certificates { get; set; }

        [XmlElement("ГРНИПДатаИспрПред")]
        public EgrIpRowGrnIpInfo GrnIpCorrection { get; set; }

        [XmlElement("ГРНИПДатаНедПред")]
        public EgrIpRowGrnIpInfo GrnIpInvalidated { get; set; }

        [XmlElement("СвСтатусЗап")]
        public EgrIpRowStatusInfo Status { get; set; }

        [XmlAttribute("ИдЗап")]
        public string RowId { get; set; }

        [XmlAttribute("ГРНИП")]
        public string GrnIp { get; set; }

        [XmlAttribute("ДатаЗап", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
