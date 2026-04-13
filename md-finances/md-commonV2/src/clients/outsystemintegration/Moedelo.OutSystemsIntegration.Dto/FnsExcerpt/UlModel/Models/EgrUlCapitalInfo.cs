using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о размере указанного в учредительных документах коммерческой организации уставного капитала (складочного капитала, уставного фонда, паевого фонда)
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlCapitalInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Доля рубля в капитале
        /// </summary>
        [XmlElement("ДоляРубля")]
        public EgrUlSimpleFractionType RubleShare { get; set; }

        /// <summary>
        /// Сведения о нахождении хозяйственного общества в процессе уменьшения уставного капитала
        /// </summary>
        [XmlElement("СведУмУК")]
        public EgrUlReductionAuthorizedCapitalInfo ReductionAuthorizedCapital { get; set; }

        /// <summary>
        /// Наименование вида капитала
        /// </summary>
        [XmlAttribute("НаимВидКап")]
        public EgrUlCapitalType CapitalType { get; set; }

        /// <summary>
        /// Размер в рублях
        /// </summary>
        [XmlAttribute(AttributeName = "СумКап", DataType = "decimal")]
        public decimal Sum { get; set; }
    }
}
