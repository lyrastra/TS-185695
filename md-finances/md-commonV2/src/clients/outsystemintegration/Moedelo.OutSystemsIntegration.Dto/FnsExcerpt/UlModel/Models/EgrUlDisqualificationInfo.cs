using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о дисквалификации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlDisqualificationInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Дата начала дисквалификации
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаНачДискв", DataType = "date")]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Дата окончания дисквалификации
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаОкончДискв", DataType = "date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Дата вынесения судебным органом постановления о дисквалификации
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаРеш", DataType = "date")]
        public DateTime DecisionDate { get; set; }
    }
}
