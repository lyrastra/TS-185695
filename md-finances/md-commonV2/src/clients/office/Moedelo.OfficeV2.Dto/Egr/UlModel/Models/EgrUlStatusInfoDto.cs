using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    public class EgrUlStatusInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения о правоспособности (статусе) юридического лица
        /// </summary>
        public EgrUlStatusDto Status { get; set; }

        /// <summary>
        /// Сведения о решении о предстоящем исключении недействующего ЮЛ из ЕГРЮЛ и его публикации
        /// </summary>
        public EgrUlDecisionToExcludeInfoDto DecisionToExclude { get; set; }
    }
}
