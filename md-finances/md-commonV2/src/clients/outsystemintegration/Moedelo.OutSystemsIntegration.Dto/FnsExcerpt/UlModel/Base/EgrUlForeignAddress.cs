using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения об адресе в РФ, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlForeignAddress : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Код страны
        /// </summary>
        [XmlAttribute("ОКСМ")]
        public string Oksm { get; set; }
        
        /// <summary>
        /// Наименование страны
        /// </summary>
        [XmlAttribute("НаимСтран")]
        public string CountryName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [XmlAttribute("АдрИн")]
        public string Address { get; set; }
    }
}
