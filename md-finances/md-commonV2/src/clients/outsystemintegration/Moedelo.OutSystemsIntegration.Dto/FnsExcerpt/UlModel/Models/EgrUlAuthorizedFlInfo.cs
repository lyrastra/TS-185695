using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о лице, имеющем право без доверенности действовать от имени юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlAuthorizedFLInfo
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
        /// Сведения о должности ФЛ
        /// </summary>
        [XmlElement("СвДолжн")]
        public EgrUlFlPositionInfo FlPosition{ get; set; }

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
        public EgrUlIdentityDocumentInfo IdentityDocument { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        [XmlElement("АдресМЖРФ")]
        public EgrUlAddress HomeAddressInRf { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        [XmlElement("АдрМЖИн")]
        public EgrUlAddress HomeAddressOutsideRf { get; set; }
        
        /// <summary>
        /// Сведения о дисквалификации
        /// </summary>
        [XmlElement("СвДискв")]
        public List<EgrUlDisqualificationInfo> DisqualificationInfo { get; set; }
    }
}
