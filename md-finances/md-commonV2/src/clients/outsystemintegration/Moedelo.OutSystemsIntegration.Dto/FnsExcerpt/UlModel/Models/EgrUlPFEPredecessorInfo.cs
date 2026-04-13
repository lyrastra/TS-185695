using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о крестьянском (фермерском) хозяйстве, на базе имущества которого создано юридическое лицо
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlPFEPredecessorInfo
    {
        /// <summary>
        /// Сведения о ФИО и (при наличии) ИНН главы КФХ
        /// </summary>
        [XmlElement("СвФЛ")]
        public EgrUlFlInfo FlInfo { get; set; }
        
        /// <summary>
        /// ОГРНИП крестьянского (фермерского) хозяйства
        /// </summary>
        [XmlAttribute("ОГРНИП")]
        public string Ogrn { get; set; }
    }
}
