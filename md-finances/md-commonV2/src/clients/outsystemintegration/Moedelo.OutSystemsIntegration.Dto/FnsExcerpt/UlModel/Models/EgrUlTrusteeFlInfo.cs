using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о доверительном управляющем - ЮЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlTrusteeFlInfo
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
    }
}
