using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителе (участнике) - физическом лице
    /// </summary>
    public class EgrUlFounderFlInfoDto
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
        /// Сведения о рождении ФЛ
        /// </summary>
        public EgrUlFlBirthInfoDto BirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        public EgrUlIdentityDocumentInfoDto IdentityDocument { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        public EgrUlAddressDto HomeAddressRf { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства за пределами территории Российской Федерации
        /// </summary>
        public EgrUlForeignAddressDto HomeAddressForeign { get; set; }

        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        public EgrUlCapitalShareInfoDto CapitalShare { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        public List<EgrUlEncumbranceShareInfoDto> EncumbranceShare { get; set; }

        /// <summary>
        /// Сведения о доверительном управляющем - ЮЛ
        /// </summary>
        public EgrUlTrusteeUlInfoDto TrusteeUl { get; set; }

        /// <summary>
        /// Сведения о доверительном управляющем - ФЛ
        /// </summary>
        public EgrUlTrusteeFlInfoDto TrusteeFl { get; set; }

        /// <summary>
        /// Сведения о лице, осуществляющем управление долей, переходящей в порядке наследования
        /// </summary>
        public EgrUlHeirShareInfoDto HeirShare { get; set; }
    }
}