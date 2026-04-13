using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения о документе, удостоверяющем личность, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlIdentityDocumentInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Код вида документа по справочнику СПДУЛ
        /// </summary>
        [XmlAttribute("КодВидДок")]
        public string CodeDocType { get; set; }

        /// <summary>
        /// Наименование вида документа
        /// </summary>
        [XmlAttribute("НаимДок")]
        public string NameDocType { get; set; }

        /// <summary>
        /// Серия и номер документа
        /// </summary>
        [XmlAttribute("СерНомДок")]
        public string SeriesNumberDoc { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаДок", DataType = "date")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        [XmlAttribute("ВыдДок")]
        public string IssueBy { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        [XmlAttribute("КодВыдДок")]
        public string DivisionCode { get; set; }
    }
}
