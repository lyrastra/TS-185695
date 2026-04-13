using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрации юридического лица в качестве страхователя в исполнительном органе Фонда социального страхования Российской Федерации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFssRegistrationInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения об исполнительном органе Фонда социального страхования Российской Федерации
        /// </summary>
        [XmlElement("СвОргФСС")]
        public EgrUlFssInfo Fss { get; set; }

        /// <summary>
        /// Регистрационный номер в исполнительном органе Фонда социального страхования Российской Федерации
        /// </summary>
        [XmlAttribute("РегНомФСС")]
        public string RegNumber { get; set; }

        /// <summary>
        /// Дата регистрации юридического лица в качестве страхователя
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаРег", DataType = "date")]
        public DateTime RegDate { get; set; }
    }
}
