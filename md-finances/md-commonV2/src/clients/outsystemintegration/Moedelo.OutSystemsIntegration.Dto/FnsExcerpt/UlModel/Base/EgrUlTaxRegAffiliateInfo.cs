using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения об учете в налоговом органе по месту нахождения обособленного подразделения (филиала/представительства)
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlTaxRegAffiliateInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения о налоговом органе по месту нахождения филиала/представительства
        /// </summary>
        [XmlElement("СвНО")]
        public EgrUlTaxAuthorityInfo TaxAuthority { get; set; }

        /// <summary>
        /// КПП филиала/представительства
        /// </summary>
        [XmlAttribute("КПП")]
        public string KPP { get; set; }

        /// <summary>
        /// Дата постановки на учет в налоговом органе
        /// </summary>
        [XmlAttribute("ДатаПостУч", DataType = "date")]
        public DateTime RegDate { get; set; }
    }
}
