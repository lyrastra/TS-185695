using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об управляющей компании паевого инвестиционного фонда
    /// </summary>
    public class EgrUlManagementCompanyMutualFundInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// СНаименование и (при наличии) ОГРН и ИНН управляющей компании паевого инвестиционного фонда
        /// </summary>
        public EgrUlBaseInfoDto ManagementCompany { get; set; }
    }
}