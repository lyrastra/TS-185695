using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpGrnIpInfo
    {
        [XmlAttribute("ГРНИП")]
        public string GrnIp { get; set; }
        
        [XmlAttribute("ДатаЗаписи", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
