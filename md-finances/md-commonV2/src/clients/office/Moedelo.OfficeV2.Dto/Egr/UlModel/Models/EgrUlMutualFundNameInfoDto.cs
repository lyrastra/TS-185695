using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о названии (индивидуальном обозначении) паевого инвестиционного фонда
    /// </summary>
    public class EgrUlMutualFundNameInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Название (индивидуальное обозначение) паевого инвестиционного фонда
        /// </summary>
        public string Name { get; set; }
    }
}