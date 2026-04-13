using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о залогодержателе - ЮЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlMortgageeUlInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        [XmlElement("ГРНДатаПерв")]
        public EgrUlGrnDateInfo Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН ЮЛ
        /// </summary>
        [XmlElement("НаимИННЮЛ")]
        public EgrUlBaseInfo UlInfo { get; set; }

        /// <summary>
        /// Сведения о регистрации в стране происхождения
        /// </summary>
        [XmlElement("СвРегИн")]
        public EgrUlForeignRegUlInfo ForeignRegInfo { get; set; }

        /// <summary>
        /// Сведения о нотариальном удостоверении договора залога
        /// </summary>
        [XmlElement("СвНотУдДогЗал")]
        public EgrUlNotarizationPledgeAgreementInfo NotarizationPledgeAgreement { get; set; }
    }
}
