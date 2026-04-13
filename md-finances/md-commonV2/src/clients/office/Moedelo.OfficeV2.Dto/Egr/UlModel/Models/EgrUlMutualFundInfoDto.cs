using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о паевом инвестиционном фонде, в состав имущества которого включена доля в уставном капитале
    /// </summary>
    public class EgrUlMutualFundInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о названии (индивидуальном обозначении) паевого инвестиционного фонда
        /// </summary>
        public EgrUlMutualFundNameInfoDto UlInfo { get; set; }
        
        /// <summary>
        /// Сведения об управляющей компании паевого инвестиционного фонда
        /// </summary>
        public EgrUlManagementCompanyMutualFundInfoDto ManagementCompany { get; set; }

        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        public EgrUlCapitalShareInfoDto CapitalShare { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        public List<EgrUlEncumbranceShareInfoDto> EncumbranceShare { get; set; }
    }
}