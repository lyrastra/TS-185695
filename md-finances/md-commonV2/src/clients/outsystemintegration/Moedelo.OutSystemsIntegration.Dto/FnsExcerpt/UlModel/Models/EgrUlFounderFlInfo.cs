using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителе (участнике) - физическом лице
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFounderFlInfo
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
        /// Сведения о рождении ФЛ
        /// </summary>
        [XmlElement("СвРождФЛ")]
        public EgrUlFlBirthInfo BirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        [XmlElement("УдЛичнФЛ")]
        public EgrUlIdentityDocumentInfo IdentityDocument { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        [XmlElement("АдресМЖРФ")]
        public EgrUlAddress HomeAddressRf { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства за пределами территории Российской Федерации
        /// </summary>
        [XmlElement("АдрМЖИн")]
        public EgrUlForeignAddress HomeAddressForeign { get; set; }

        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        [XmlElement("ДоляУстКап")]
        public EgrUlCapitalShareInfo CapitalShare { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        [XmlElement("СвОбрем")]
        public List<EgrUlEncumbranceShareInfo> EncumbranceShare { get; set; }

        /// <summary>
        /// Сведения о доверительном управляющем - ЮЛ
        /// </summary>
        [XmlElement("СвДовУпрЮЛ")]
        public EgrUlTrusteeUlInfo TrusteeUl { get; set; }

        /// <summary>
        /// Сведения о доверительном управляющем - ФЛ
        /// </summary>
        [XmlElement("СвДовУпрФЛ")]
        public EgrUlTrusteeFlInfo TrusteeFl { get; set; }

        /// <summary>
        /// Сведения о лице, осуществляющем управление долей, переходящей в порядке наследования
        /// </summary>
        [XmlElement("ЛицоУпрНасл")]
        public EgrUlHeirShareInfo HeirShare { get; set; }
    }
}
