using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о видах экономической деятельности по Общероссийскому классификатору видов экономической деятельности
    /// </summary>
    public class EgrUlOkvedInfoDto
    {
        /// <summary>
        /// Сведения об основном виде деятельности
        /// </summary>
        public EgrUlOkvedDto OkvedMain { get; set; }

        /// <summary>
        /// Сведения о дополнительном виде деятельности
        /// </summary>
        public List<EgrUlOkvedDto> OkvedOther { get; set; }
    }
}