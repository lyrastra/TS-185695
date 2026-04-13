using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об исполнительном органе Фонда социального страхования Российской Федерации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFssInfo
    {
        /// <summary>
        /// Код по справочнику СТОФСС
        /// </summary>
        [XmlAttribute("КодФСС")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [XmlAttribute("НаимФСС")]
        public string Name { get; set; }
    }
}
