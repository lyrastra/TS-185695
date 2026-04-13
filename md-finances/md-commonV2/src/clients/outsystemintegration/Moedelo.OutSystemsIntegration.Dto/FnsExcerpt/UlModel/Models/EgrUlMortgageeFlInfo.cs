using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о залогодержателе - ФЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlMortgageeFlInfo
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
        /// Сведения о ФИО и (при наличии) ИНН ФЛ
        /// </summary>
        [XmlElement("СвРождФЛ")]
        public EgrUlFlBirthInfo FlBirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        [XmlElement("УдЛичнФЛ")]
        public EgrUlIdentityDocumentInfo FlIdentityDocument { get; set; }

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
        /// Сведения о нотариальном удостоверении договора залога
        /// </summary>
        [XmlElement("СвНотУдДогЗал")]
        public EgrUlNotarizationPledgeAgreementInfo NotarizationPledgeAgreement { get; set; }
    }
}
