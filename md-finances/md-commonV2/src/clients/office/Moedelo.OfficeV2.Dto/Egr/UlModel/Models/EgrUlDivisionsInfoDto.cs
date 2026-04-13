using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об обособленных подразделениях юридического лица
    /// </summary>
    public class EgrUlDivisionsInfoDto
    {
        /// <summary>
        /// Сведения о филиалах юридического лица
        /// </summary>
        public List<EgrUlAffiliateInfoDto> Affiliate { get; set; }

        /// <summary>
        /// Сведения о представительствах юридического лица
        /// </summary>
        public List<EgrUlAgencyInfoDto> Agency { get; set; }
    }
}