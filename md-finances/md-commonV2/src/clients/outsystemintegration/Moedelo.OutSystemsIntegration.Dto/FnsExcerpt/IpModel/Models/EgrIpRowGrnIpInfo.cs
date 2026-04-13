using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpRowGrnIpInfo : EgrIpGrnIpInfo
    {
        [XmlAttribute("ИдЗап")]
        public string RowId { get; set; }
    }
}
