using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Base
{
    /// <summary>
    /// Сведения о регистрирующем органе
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrIpReorgOrgInfo
    {
        /// <summary>
        /// Код органа по справочнику СОНО
        /// </summary>
        [XmlAttribute("КодНО")]
        public string Code { get; set; }
        
        /// <summary>
        /// Наименование регистрирующего (налогового) органа
        /// </summary>
        [XmlAttribute("НаимНО")]
        public string Name { get; set; }
    }
}
