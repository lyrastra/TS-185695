using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о нахождении хозяйственного общества в процессе уменьшения уставного капитала
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlReductionAuthorizedCapitalInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Величина, на которую уменьшается уставный капитал (в рублях)
        /// </summary>
        [XmlAttribute(AttributeName = "ВелУмУК", DataType = "decimal")]
        public decimal Value { get; set; }

        /// <summary>
        /// Дата принятия решения об уменьшении уставного капитала
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаРеш", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
