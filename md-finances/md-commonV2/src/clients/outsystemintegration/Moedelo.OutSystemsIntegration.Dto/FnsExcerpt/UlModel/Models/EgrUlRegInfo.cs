using System;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrUlRegInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Способ образования юридического лица
        /// </summary>
        [XmlElement("СпОбрЮЛ")]
        public EgrUlMethodFormingInfo MethodForming { get; set; }

        /// <summary>
        /// Основной государственный регистрационный номер юридического лица
        /// </summary>
        [XmlAttribute("ОГРН")]
        public string Ogrn { get; set; }
        
        /// <summary>
        /// Дата присвоения ОГРН
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаОГРН", DataType = "date")]
        public DateTime OgrnDate { get; set; }
        
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        [XmlAttribute("РегНом")]
        public string RegNumber { get; set; }
        
        /// <summary>
        /// Дата регистрации юридического лица
        /// </summary>
        [XmlAttribute(AttributeName = "ДатаРег", DataType = "date")]
        public DateTime RegDate { get; set; }

        [XmlIgnore]
        public bool RegDateSpecified { get; set; }
        
        /// <summary>
        /// Наименование органа, зарегистрировавшего юридическое лицо
        /// </summary>
        [XmlAttribute("НаимРО")]
        public string RegAuthorityName { get; set; }
    }
}
