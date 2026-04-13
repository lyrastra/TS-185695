using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрации учредителя (участника) до 01.07.2002 г
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlOldRegInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Регистрационный номер, присвоенный юридическому лицу до 1 июля 2002 года
        /// </summary>
        [XmlAttribute("РегНом")]
        public string RegNumber { get; set; }

        /// <summary>
        /// Дата регистрации юридического лица до 1 июля 2002 года
        /// </summary>
        [XmlAttribute("ДатаРег", DataType = "date")]
        public DateTime RegDate { get; set; }

        [XmlIgnore]
        public bool RegDateSpecified { get; set; }

        /// <summary>
        /// Наименование органа, зарегистрировавшего юридическое лицо до 1 июля 2002 года
        /// </summary>
        [XmlAttribute("НаимРО")]
        public string RegAuthorityName { get; set; }
    }
}
