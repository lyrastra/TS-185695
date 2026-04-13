using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрации юридического лица в качестве страхователя в территориальном органе Пенсионного фонда Российской Федерации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlPFRegistrationInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения о территориальном органе Пенсионного фонда Российской Федерации
        /// </summary>
        [XmlElement("СвОргПФ")]
        public EgrUlPfInfo PfInfo { get; set; }

        /// <summary>
        /// Регистрационный номер в территориальном органе Пенсионного фонда Российской Федерации
        /// </summary>
        [XmlAttribute("РегНомПФ")]
        public string RegNumber { get; set; }

        /// <summary>
        /// Дата регистрации юридического лица в качестве страхователя
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаРег", DataType = "date")]
        public DateTime RegDate { get; set; }
    }
}
