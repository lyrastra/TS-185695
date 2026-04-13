using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о наименовании филиала
    /// </summary>
    public class EgrUlAffiliateNameInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном филиале
        /// </summary>
        public string Name { get; set; }
    }
}