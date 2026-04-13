using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о приостановлении действия лицензии
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlLicenseSuspensionInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Дата приостановления действия лицензии
        /// </summary>
        [XmlAttribute("ДатаПриостЛиц", DataType = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Наименование лицензирующего органа, приостановившего действие лицензии
        /// </summary>
        [XmlAttribute("ЛицОргПриостЛиц")]
        public string Licensor { get; set; }
    }
}
