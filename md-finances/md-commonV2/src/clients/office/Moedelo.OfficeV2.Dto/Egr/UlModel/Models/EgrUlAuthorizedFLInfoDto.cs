using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о лице, имеющем право без доверенности действовать от имени юридического лица
    /// </summary>
    public class EgrUlAuthorizedFlInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о ФИО и (при наличии) ИНН ФЛ
        /// </summary>
        public EgrUlFlInfoDto FlInfo { get; set; }

        /// <summary>
        /// Сведения о должности ФЛ
        /// </summary>
        public EgrUlFlPositionInfoDto FlPosition{ get; set; }

        /// <summary>
        /// Сведения о контактном телефоне ФЛ
        /// </summary>
        public EgrUlPhoneInfoDto Phone { get; set; }

        /// <summary>
        /// Сведения о рождении ФЛ
        /// </summary>
        public EgrUlFlBirthInfoDto FlBirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        public EgrUlIdentityDocumentInfoDto IdentityDocument { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        public EgrUlAddressDto HomeAddressInRf { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        public EgrUlAddressDto HomeAddressOutsideRf { get; set; }
        
        /// <summary>
        /// Сведения о дисквалификации
        /// </summary>
        public List<EgrUlDisqualificationInfoDto> DisqualificationInfo { get; set; }
    }
}