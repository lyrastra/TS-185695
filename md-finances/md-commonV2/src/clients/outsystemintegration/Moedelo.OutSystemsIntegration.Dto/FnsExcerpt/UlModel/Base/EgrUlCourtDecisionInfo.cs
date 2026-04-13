using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Решение суда
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlCourtDecisionInfo
    {
        /// <summary>
        /// Наименование суда
        /// </summary>
        [XmlAttribute("НаимСуда")]
        public string Name { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [XmlAttribute("Номер")]
        public string Number { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [XmlAttribute(AttributeName = "Дата", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
