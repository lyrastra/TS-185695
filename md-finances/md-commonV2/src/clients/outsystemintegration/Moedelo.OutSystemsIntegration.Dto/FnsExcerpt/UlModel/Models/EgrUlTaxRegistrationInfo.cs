using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об учете в налоговом органе
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlTaxRegistrationInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения о налоговом органе, в котором юридическое лицо состоит (для ЮЛ, прекративших деятельность - состояло) на учете
        /// </summary>
        [XmlElement("СвНО")]
        public EgrUlTaxAuthorityInfo TaxAuthority { get; set; }

        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        [XmlAttribute("ИНН")]
        public string Inn { get; set; }

        /// <summary>
        /// КПП юридического лица
        /// </summary>
        [XmlAttribute("КПП")]
        public string Kpp { get; set; }

        /// <summary>
        /// Дата постановки на учет в налоговом органе
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаПостУч", DataType = "date")]
        public DateTime RegistrationDate { get; set; }
    }
}
