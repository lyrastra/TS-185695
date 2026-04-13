using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о размере указанного в учредительных документах коммерческой организации уставного капитала (складочного капитала, уставного фонда, паевого фонда)
    /// </summary>
    public class EgrUlCapitalInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Доля рубля в капитале
        /// </summary>
        public EgrUlSimpleFractionTypeDto RubleShare { get; set; }

        /// <summary>
        /// Сведения о нахождении хозяйственного общества в процессе уменьшения уставного капитала
        /// </summary>
        public EgrUlReductionAuthorizedCapitalInfoDto ReductionAuthorizedCapital { get; set; }

        /// <summary>
        /// Наименование вида капитала
        /// </summary>
        public EgrUlCapitalType CapitalType { get; set; }

        /// <summary>
        /// Размер в рублях
        /// </summary>
        public decimal Sum { get; set; }
    }
}