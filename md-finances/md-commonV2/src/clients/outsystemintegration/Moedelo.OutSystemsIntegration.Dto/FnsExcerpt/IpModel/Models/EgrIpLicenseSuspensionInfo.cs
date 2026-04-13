using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpLicenseSuspensionInfo : EgrUlGrnDateBaseInfo
    {
        [XmlAttribute("ДатаПриостЛиц", DataType = "date")]
        public DateTime Date { get; set; }

        [XmlAttribute("ЛицОргПриостЛиц")]
        public string Licensor { get; set; }
    }
}
