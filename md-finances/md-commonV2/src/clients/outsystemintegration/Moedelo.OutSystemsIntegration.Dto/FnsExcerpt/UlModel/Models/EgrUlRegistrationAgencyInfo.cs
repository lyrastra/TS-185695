using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрирующем органе по месту нахождения юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlRegistrationAgencyInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Код органа по справочнику СОНО
        /// </summary>
        [XmlAttribute("КодНО")]
        public string Code { get; set; }
        
        /// <summary>
        /// Наименование регистрирующего (налогового) органа
        /// </summary>
        [XmlAttribute("НаимНО")]
        public string Name { get; set; }
        
        /// <summary>
        /// Адрес регистрирующего органа
        /// </summary>
        [XmlAttribute("АдрРО")]
        public string Address { get; set; }
    }
}
