using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о форме реорганизации (статусе) юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlReorgStatusInfo
    {
        /// <summary>
        /// Код формы реорганизации (статуса) юридического лица по справочнику СЮЛСТ
        /// </summary>
        [XmlAttribute("КодСтатусЮЛ")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование формы реорганизации (статуса) юридического лица по справочнику СЮЛСТ
        /// </summary>
        [XmlAttribute("НаимСтатусЮЛ")]
        public string Name { get; set; }
    }
}
