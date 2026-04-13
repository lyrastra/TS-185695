using System;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения о регистрации иностранного ЮЛ в стране происхождения, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlForeignRegUlInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Код страны происхождения
        /// </summary>
        [XmlAttribute("ОКСМ")]
        public string Oksm { get; set; }

        /// <summary>
        /// Наименование страны происхождения
        /// </summary>
        [XmlAttribute("НаимСтран")]
        public string CountryName { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        [XmlAttribute("ДатаРег")]
        public DateTime RegDate { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        [XmlAttribute("РегНомер")]
        public string RegNumber { get; set; }

        /// <summary>
        /// Наименование регистрирующего органа
        /// </summary>
        [XmlAttribute("НаимРегОрг")]
        public string RegOrgName { get; set; }

        /// <summary>
        /// Адрес (место нахождения) в стране происхождения
        /// </summary>
        [XmlAttribute("АдрСтр")]
        public string Address { get; set; }
    }
}
