using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о лицензиях, выданных ЮЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlLicenseInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Наименование лицензируемого вида деятельности, на который выдана лицензия
        /// </summary>
        [XmlElement("НаимЛицВидДеят")]
        public List<string> LicenseTypeName { get; set; }

        /// <summary>
        /// Сведения об адресе места осуществления лицензируемого вида деятельности
        /// </summary>
        [XmlElement("МестоДейстЛиц")]
        public List<string> PlaceLicenseActivity { get; set; }

        /// <summary>
        /// Наименование лицензирующего органа, выдавшего или переоформившего лицензию
        /// </summary>
        [XmlElement("ЛицОргВыдЛиц")]
        public string Licensor { get; set; }

        /// <summary>
        /// Сведения о приостановлении действия лицензии
        /// </summary>
        [XmlElement("СвПриостЛиц")]
        public EgrUlLicenseSuspensionInfo LicenseSuspension { get; set; }

        /// <summary>
        /// Серия лицензии
        /// </summary>
        [XmlAttribute("СерЛиц")]
        public string LicenseSeries { get; set; }

        /// <summary>
        /// Номер лицензии
        /// </summary>
        [XmlAttribute("НомЛиц")]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Вид лицензии
        /// </summary>
        [XmlAttribute("ВидЛиц")]
        public string LicenseType { get; set; }

        /// <summary>
        /// Дата лицензии
        /// </summary>
        [XmlAttribute("ДатаЛиц", DataType = "date")]
        public DateTime LicenseDate { get; set; }

        /// <summary>
        /// Дата начала действия лицензии
        /// </summary>
        [XmlAttribute("ДатаНачЛиц", DataType = "date")]
        public DateTime LicenseStartDate { get; set; }

        /// <summary>
        /// Дата окончания действия лицензии
        /// </summary>
        [XmlAttribute("ДатаОкончЛиц", DataType = "date")]
        public DateTime LicenseEndDate { get; set; }

        /// <summary>
        /// Наличие даты окончания действия лицензии
        /// </summary>
        [XmlIgnore]
        public bool LicenseEndDateSpecified { get; set; }
    }
}
