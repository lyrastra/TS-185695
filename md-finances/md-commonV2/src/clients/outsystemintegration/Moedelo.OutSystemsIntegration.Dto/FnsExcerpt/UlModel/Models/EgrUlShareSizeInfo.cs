using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Размер доли (в процентах или в виде дроби - десятичной или простой)
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlShareSizeInfo
    {
        /// <summary>
        /// Размер доли в процентах
        /// </summary>
        [XmlElement("Процент")]
        public decimal Persent { get; set; }

        /// <summary>
        /// Размер доли в десятичных дробях
        /// </summary>
        [XmlElement("ДробДесят")]
        public decimal DecimalFraction { get; set; }

        /// <summary>
        /// Размер доли в простых дробях
        /// </summary>
        [XmlElement("ДробПрост")]
        public EgrUlSimpleFractionType SimpleFraction { get; set; }
    }
}
