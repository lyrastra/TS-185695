using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о лице, через которое иностранное юридическое лицо осуществляет полномочия управляющей организации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlRepresentativeInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Сведения о ФИО и (при наличии) ИНН ФЛ
        /// </summary>
        [XmlElement("СвФЛ")]
        public EgrUlFlInfo FlInfo { get; set; }

        /// <summary>
        /// Сведения о контактном телефоне ФЛ
        /// </summary>
        [XmlElement("СвНомТел")]
        public EgrUlPhoneInfo Phone { get; set; }

        /// <summary>
        /// Сведения о рождении ФЛ
        /// </summary>
        [XmlElement("СвРождФЛ")]
        public EgrUlFlBirthInfo FlBirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        [XmlElement("УдЛичнФЛ")]
        public EgrUlIdentityDocumentInfo IdentityDoc { get; set; }
    }
}
