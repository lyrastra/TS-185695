using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlGrnDateInfo
    {
        /// <summary>
        /// Государственный регистрационный номер записи ЕГРЮЛ
        /// </summary>
        [XmlAttribute("ГРН")]
        public string Number { get; set; }

        /// <summary>
        /// Дата внесения записи в ЕГРЮЛ
        /// </summary>
        [XmlAttribute("ДатаЗаписи", DataType = "date")]
        public DateTime Date { get; set; }
    }
}
