using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrUlDecisionToExcludeInfo
    {
        /// <summary>
        /// Дата решения
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаРеш", DataType = "date")]
        public System.DateTime DecisionDate { get; set; }

        /// <summary>
        /// Номер решения
        /// </summary>
        [XmlAttribute("НомерРеш")]
        public string DecisionNumber { get; set; }

        /// <summary>
        /// Дата публикации решения
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаПубликации", DataType = "date")]
        public System.DateTime PublicationDate { get; set; }

        /// <summary>
        /// Номер журнала, в котором опубликовано решение
        /// </summary>
        [XmlAttribute("НомерЖурнала")]
        public string JournalNumber { get; set; }
    }
}
