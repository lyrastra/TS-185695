using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения о контактном телефоне
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = true)]
    public class EgrUlPhoneInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Контактный телефон
        /// </summary>
        [XmlAttribute("НомТел")]
        public string PhoneNamber { get; set; }
    }
}
