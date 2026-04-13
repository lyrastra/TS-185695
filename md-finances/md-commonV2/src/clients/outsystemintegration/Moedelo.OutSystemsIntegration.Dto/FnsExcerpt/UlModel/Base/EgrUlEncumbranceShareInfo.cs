using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    /// <summary>
    /// Сведения об обременении доли участника, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlEncumbranceShareInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения о решении судебного органа, по которому на долю учредителя (участника) наложено обременение
        /// </summary>
        [XmlElement("РешСуд")]
        public EgrUlCourtDecisionInfo CourtDecision { get; set; }

        /// <summary>
        /// Сведения о залогодержателе - ФЛ
        /// </summary>
        [XmlElement("СвЗалогДержФЛ")]
        public EgrUlMortgageeFlInfo MortgageeFlInfo { get; set; }

        /// <summary>
        /// Сведения о залогодержателе - ЮЛ
        /// </summary>
        [XmlElement("СвЗалогДержЮЛ")]
        public EgrUlMortgageeUlInfo MortgageeUlInfo { get; set; }

        /// <summary>
        /// Вид обременения
        /// </summary>
        [XmlAttribute("ВидОбрем")]
        public EgrUlEncumbranceType EncumbranceType { get; set; }

        /// <summary>
        /// Срок обременения или порядок определения срока
        /// </summary>
        [XmlAttribute("СрокОбременения")]
        public string EncumbranceTerm { get; set; }
    }
}
