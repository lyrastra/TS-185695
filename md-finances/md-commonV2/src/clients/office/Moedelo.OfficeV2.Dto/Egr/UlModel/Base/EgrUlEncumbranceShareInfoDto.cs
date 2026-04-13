using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Models;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения об обременении доли участника, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrUlEncumbranceShareInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения о решении судебного органа, по которому на долю учредителя (участника) наложено обременение
        /// </summary>
        public EgrUlCourtDecisionInfoDto CourtDecision { get; set; }

        /// <summary>
        /// Сведения о залогодержателе - ФЛ
        /// </summary>
        public EgrUlMortgageeFlInfoDto MortgageeFlInfo { get; set; }

        /// <summary>
        /// Сведения о залогодержателе - ЮЛ
        /// </summary>
        public EgrUlMortgageeUlInfoDto MortgageeUlInfo { get; set; }

        /// <summary>
        /// Вид обременения
        /// </summary>
        public EgrUlEncumbranceType EncumbranceType { get; set; }

        /// <summary>
        /// Срок обременения или порядок определения срока
        /// </summary>
        public string EncumbranceTerm { get; set; }
    }
}
