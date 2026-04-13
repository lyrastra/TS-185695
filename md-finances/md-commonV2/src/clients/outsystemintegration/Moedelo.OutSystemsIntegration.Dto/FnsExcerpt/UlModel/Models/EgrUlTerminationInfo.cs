using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrUlTerminationInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Способ прекращения юридического лица
        /// </summary>
        [XmlElement("СпПрекрЮЛ")]
        public EgrUlTerminationMethodInfo TerminationMethod { get; set; }

        /// <summary>
        /// Сведения о регистрирующем (налоговом) органе, внесшем запись о прекращении юридического лица
        /// </summary>
        [XmlElement("СвРегОрг")]
        public EgrUlRegisteringTaxAuthorityInfo RegisteringTaxAuthority { get; set; }

        /// <summary>
        /// Дата прекращения юридического лица
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаПрекрЮЛ", DataType = "date")]
        public DateTime TerminationDate { get; set; }
    }
}
