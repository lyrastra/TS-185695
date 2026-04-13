using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения о рождении ФЛ, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlFlBirthInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Дата рождения
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаРожд", DataType = "date")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        [XmlAttribute("МестоРожд")]
        public string BirthPlace { get; set; }

        /// <summary>
        /// Признак полноты представляемой даты рождения физического лица
        /// </summary>
        [XmlAttribute(AttributeName = "ПрДатаРожд")]
        public EgrUlSignCompletenessBirthDate SignCompletenessBirthDate { get; set; }
    }
}
