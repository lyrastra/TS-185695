using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о названии (индивидуальном обозначении) паевого инвестиционного фонда
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlMutualFundNameInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Название (индивидуальное обозначение) паевого инвестиционного фонда
        /// </summary>
        [XmlAttribute("НаимПИФ")]
        public string Name { get; set; }
    }
}
