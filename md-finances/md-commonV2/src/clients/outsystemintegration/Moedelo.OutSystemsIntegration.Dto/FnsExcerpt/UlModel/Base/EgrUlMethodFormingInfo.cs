using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Способ образования юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlMethodFormingInfo
    {
        /// <summary>
        /// Код способа образования по справочнику СЮЛНД
        /// </summary>
        [XmlAttribute("КодСпОбрЮЛ")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование способа образования юридического лица
        /// </summary>
        [XmlAttribute("НаимСпОбрЮЛ")]
        public string Name { get; set; }
    }
}
