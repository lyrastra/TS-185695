using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Представление простой дроби
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlSimpleFractionType
    {
        /// <summary>
        /// Числитель простой дроби
        /// </summary>
        [XmlAttribute("Числит")]
        public string Numerator { get; set; }

        /// <summary>
        /// Знаменатель простой дроби
        /// </summary>
        [XmlAttribute("Знаменат")]
        public string Denominator { get; set; }
    }
}
