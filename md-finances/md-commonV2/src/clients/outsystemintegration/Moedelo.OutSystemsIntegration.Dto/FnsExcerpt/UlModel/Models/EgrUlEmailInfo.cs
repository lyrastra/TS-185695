using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об адресе электронной почты юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlEmailInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [XmlAttribute("E-mail")]
        public string Email { get; set; }
    }
}
