using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    [XmlType(AnonymousType = true)]
    public class EgrUlTerminationMethodInfo
    {
        /// <summary>
        /// Код способа прекращения по справочнику СЮЛПД
        /// </summary>
        [XmlAttribute("КодСпПрекрЮЛ")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование способа прекращения
        /// </summary>
        [XmlAttribute("НаимСпПрекрЮЛ")]
        public string Name { get; set; }
    }
}
