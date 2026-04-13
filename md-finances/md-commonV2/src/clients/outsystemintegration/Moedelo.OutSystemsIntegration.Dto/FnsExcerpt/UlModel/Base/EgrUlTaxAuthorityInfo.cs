using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения о налоговом органе, в котором юридическое лицо или обособленное подразделение состоит (состояло) на учете
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlTaxAuthorityInfo
    {
        /// <summary>
        /// Код органа по справочнику СОНО
        /// </summary>
        [XmlAttribute("КодНО")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование налогового органа
        /// </summary>
        [XmlAttribute("НаимНО")]
        public string Name { get; set; }
    }
}
