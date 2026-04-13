using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrIpTaxRegInfo : EgrIpGrnIpBase
    {
        [XmlElement("СвНО")]
        public EgrIpTaxAuthorityInfo TaxAuthority { get; set; }

        [XmlAttribute("ИННФЛ")]
        public string InnFl { get; set; }

        [XmlAttribute("ДатаПостУч", DataType = "date")]
        public DateTime RegDate { get; set; }
    }
}
