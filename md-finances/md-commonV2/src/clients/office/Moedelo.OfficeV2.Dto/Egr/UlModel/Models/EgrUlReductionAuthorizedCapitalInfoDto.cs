using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о нахождении хозяйственного общества в процессе уменьшения уставного капитала
    /// </summary>
    public class EgrUlReductionAuthorizedCapitalInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Величина, на которую уменьшается уставный капитал (в рублях)
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Дата принятия решения об уменьшении уставного капитала
        /// </summary>
        public DateTime Date { get; set; }
    }
}