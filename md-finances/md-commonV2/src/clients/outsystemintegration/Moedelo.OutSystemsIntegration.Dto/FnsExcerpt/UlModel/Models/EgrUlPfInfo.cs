using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о территориальном органе Пенсионного фонда Российской Федерации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlPfInfo
    {
        /// <summary>
        /// Код по справочнику СТОПФ
        /// </summary>
        [XmlAttribute("КодПФ")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [XmlAttribute("НаимПФ")]
        public string Name { get; set; }
    }
}
