using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    /// <summary>
    /// Сведения о регистрирующем органе
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpRegTaxAuthorityInfo
    {
        /// <summary>
        /// Код вида документа по справочнику СПДУЛ
        /// </summary>
        [XmlAttribute("КодВидДок")]
        public string CodeDocType { get; set; }

        /// <summary>
        /// Наименование документа по справочнику СПДУЛ
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

        [XmlIgnore]
        public bool IssueDateSpecified { get; set; }

        /// <summary>
        /// Кем выдан документ
        /// </summary>
        [XmlAttribute("ВыдДок")]
        public string IssueBy { get; set; }

        /// <summary>
        /// Код подразделения, выдавшего документ
        /// </summary>
        [XmlAttribute("КодВыдДок")]
        public string DivisionCode { get; set; }
    }
}
