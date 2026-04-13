using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrUlStatus
    {
        /// <summary>
        /// Код статуса юридического лица по справочнику СЮЛСТ
        /// </summary>
        [XmlAttribute("КодСтатусЮЛ")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование статуса юридического лица по справочнику СЮЛСТ
        /// </summary>
        [XmlAttribute("НаимСтатусЮЛ")]
        public string Name { get; set; }
    }
}
